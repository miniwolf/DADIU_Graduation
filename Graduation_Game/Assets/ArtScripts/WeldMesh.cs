using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeldMesh : MonoBehaviour {
    [Tooltip("Cutoff value for dot product")]
    [Range(0, 1)]
    public float normalNoiseCutoff = 0.8f;

    [Tooltip("Blend range for dot product")]
    [Range(0, 1)]
    public float normalNoiseBlend = 0.2f;

    // Used to check if vertex can be excluded from noise calculations
    public float normalCutoff {
        get { return normalNoiseCutoff - normalNoiseBlend; }
    }
	[Range(0, 1.4f)]
    [Tooltip("Size of noise pattern")]
    public float noiseScale = 0.5f;

    [Tooltip("Depth of noise pattern")]
    public float noisePower = 0.5f;

    [Tooltip("Power of minor noise on all surfaces")]
    public float baseNoisePower = 0.05f;

    [Tooltip("Offset for perlin noise")]
    public float randomSeed = 0;

    [Tooltip("Meshes that will be combined")]
    public MeshFilter[] meshFilters;


	void Start () {
		CombineMeshesAndAddNoise();
	}


    //External method to call with all meshfilters that should be combined
    public void CombineMeshes(MeshFilter[] meshFilters) {
        this.meshFilters = meshFilters;
        CombineMeshesAndAddNoise();
    }
    //External method to call to combine meshfilters in the list already
    public void CombineMeshesAndAddNoise() {
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        // Add meshes from meshFilters to combine 
        for (int idx = 0; idx < meshFilters.Length; idx++) {
            combine[idx].mesh = meshFilters[idx].sharedMesh;
            combine[idx].transform = meshFilters[idx].transform.localToWorldMatrix;
            // Disable renderer on meshfilter
            meshFilters[idx].GetComponent<Renderer>().enabled = false;
            // or destroy (OnValidate requires objects to stay as reference for rebuild)
            //Destroy(meshFilters[idx].GetComponent<Renderer>());
            //Destroy(meshFilters[idx]);
        }

        //Combine meshes
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);

        // Fix normals on some buggy meshes
        mesh.RecalculateNormals();

        // Weld verticies by removing duplicate verticies indexes from triangles list
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 curVert = vertices[i];
            for (int j = 0; j < vertices.Length; j++) {
                // A vertex cannot weld to itself, continue
                if (i == j) continue;
                if (Vector3.Distance(curVert, vertices[j]) < 0.001f) {
                    // Weld verts, replace all instances of j in triangles with i
                    for (int k = 0; k < triangles.Length; k++) {
                        if (triangles[k] == j) triangles[k] = i; 
                    }
                }
            }
        }
        mesh.triangles = triangles;

        // Add noise based on normals, calculate dot between normal and world directions
        // use dot product as cutoff and blend value as 0f-1f weight of noise power
        Vector3[] norms = mesh.normals;
        for (int h = 0; h < vertices.Length; h++) {
            // Front side noise
            float forwardDot = Vector3.Dot(Vector3.forward, norms[h]);
            if (forwardDot > normalCutoff || forwardDot < -normalCutoff) {
                if (forwardDot < 0) forwardDot = -forwardDot;
                float perlinNoise = (Mathf.PerlinNoise(randomSeed + vertices[h].x * noiseScale, randomSeed + vertices[h].y * noiseScale) - 0.5f) * 2;
                vertices[h].z += perlinNoise * noisePower * GetDotWeight(forwardDot);
            }
            // Right and left side noise
            float rightDot = Vector3.Dot(Vector3.right, norms[h]);
            if (rightDot > normalCutoff || rightDot < -normalCutoff) {
                if (rightDot < 0) rightDot = -rightDot;
                float perlinNoise = (Mathf.PerlinNoise(randomSeed + vertices[h].z * noiseScale, randomSeed + vertices[h].y * noiseScale) - 0.5f) * 2;
                vertices[h].x += perlinNoise * noisePower * GetDotWeight(rightDot);
            }
            // Bottom side noise
            float botDot = Vector3.Dot(-Vector3.up, norms[h]);
            if (botDot > normalCutoff) {
                float perlinNoise = (Mathf.PerlinNoise(randomSeed + vertices[h].x * noiseScale, randomSeed + vertices[h].z * noiseScale) - 0.5f) * 2;
                vertices[h].y += perlinNoise * noisePower * GetDotWeight(botDot);
            }
            // Minor generic noise for all surfaces
            vertices[h] += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * baseNoisePower;
        }
        mesh.vertices = vertices;
        
        // Recalculate normals for a smooth surface
        mesh.RecalculateNormals();

        transform.GetComponent<MeshFilter>().mesh = mesh;
        transform.gameObject.SetActive(true);
    }

    // Get 0f-1f weight of noise based on cutoff and blend values
    float GetDotWeight(float dot) {
        float f = dot - (normalNoiseCutoff - normalNoiseBlend);
        f *= 1f / normalNoiseBlend;
        return Mathf.Clamp01(f);
    }

    // Allows value changes in inspector during playmode
    void OnValidate() {
        //if (Application.isPlaying) {
            GetComponent<MeshFilter>().mesh = null;
            CombineMeshesAndAddNoise();
        //}
    }
}
