using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;


public class SlopeGenerator : MonoBehaviour
{
    public AnimationCurve slopeCurve;

    private int width = 10, height = 10;
    private int vertPerLine = 10;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Generate()
    {
        MeshRenderer [] existing = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer t in existing)
        {
            DestroyImmediate(t.gameObject);
        }

        int length = 100;
        int width = 2;

//        var data = new MeshData(length, width);
//        int vertexIndex = 0;

//        for (int y = 0; y < height; y++)
//        {
//            for (int x = 0; x < 2; x++)
//            {
//                data.vertices[vertexIndex] = new Vector3(x, slopeCurve.Evaluate(x), y);
//                data.uvs[vertexIndex] = new Vector2(x / (float) width, y / (float) height);
//
//                if (x < width - 1 && y < height - 1)
//                {
//                    // ignore right and bottom vertices
//                    data.AddTriangle(vertexIndex, vertexIndex + vertPerLine + 1, vertexIndex + vertPerLine);
//                    data.AddTriangle(vertexIndex + vertPerLine + 1, vertexIndex, vertexIndex + 1);
//                }
//                vertexIndex++;
//            }
//        }
//
//        var mesh = new Mesh();
//        mesh.vertices = data.vertices;
//        mesh.triangles = data.triangles;
//        mesh.uv = data.uvs;
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();
//
//        GameObject slope = new GameObject();
//        slope.transform.parent = this.transform;
//        MeshFilter meshFilter = slope.AddComponent<MeshFilter>();
//        MeshRenderer renderer = slope.AddComponent<MeshRenderer>();
//        renderer.material.color = Color.white;
//        meshFilter.sharedMesh = mesh;


         for (int x = 0; x < length; x++)
         {
             float pos = x / (float) length;
             float y = length * slopeCurve.Evaluate(pos);

             Vector3 start  = new Vector3(x,y , 0);
             Vector3 end = new Vector3(x + 1, length * slopeCurve.Evaluate((x + 1) / (float) length), 0f);

             {
                 GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                 GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                 cube1.transform.position = start;
                 cube1.transform.localScale = Vector3.one / 3;

                 start.z = start.z + width;
                 cube2.transform.position = start;
                 cube2.transform.localScale = Vector3.one / 3;

                 cube1.transform.parent = transform;
                 cube2.transform.parent = transform;
             }
         }
    }

    public class MeshData
    {
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;

        int index;

        public MeshData(int width, int height)
        {
            vertices = new Vector3[width * height];
            uvs = new Vector2[width * height];
            triangles = new int[(width - 1) * (height - 1) * 2 * 3];
            // number of triangles in the map, number of squares * 2 triangles * 3 vertices
        }

        public void AddTriangle(int a, int b, int c)
        {
            triangles[index] = a;
            triangles[index + 1] = b;
            triangles[index + 2] = c;
            index += 3;
        }

        public Mesh CreateMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;

            mesh.RecalculateNormals();
            return mesh;
        }
    }
}