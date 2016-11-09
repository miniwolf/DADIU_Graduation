using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;


public class SlopeGenerator : MonoBehaviour
{
    public AnimationCurve slopeCurve;

    private Vector3 scale = Vector3.one / 3;
    private int vertPerLine = 1;
    // Use this for initialization

    public void Generate()
    {
        MeshRenderer[] existing = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer t in existing)
        {
            DestroyImmediate(t.gameObject);
        }

        int length = 100;
        int width = 2;

        var data = new MeshData(length, 2);
        int vertexIndex = 0;

        float minY = float.MaxValue;
        float startX = 0, endX = length;

        for (int x = 0; x < length; x++) // generate curve
        {
            float pos = x / (float) length;
            float y = length * slopeCurve.Evaluate(pos);

            if (y < minY)
                minY = y;

            Vector3 start = new Vector3(x, y, 0);
//            Vector3 end = new Vector3(x + 1, length * slopeCurve.Evaluate((x + 1) / (float) length), 0f);
            {
                PlaceCube(start);
                data.vertices[vertexIndex] = start;
                data.uvs[vertexIndex] = new Vector2(x / (float) length, 0);
//                data.AddTriangle(vertexIndex, vertexIndex + vertPerLine + 1, vertexIndex + vertPerLine);
//                data.AddTriangle(vertexIndex + vertPerLine + 1, vertexIndex, vertexIndex + 1);
                vertexIndex++;

                start.z = start.z + width;

                PlaceCube(start);
                data.vertices[vertexIndex] = start;
                data.uvs[vertexIndex] = new Vector2(x / (float) length, 1);
//                data.AddTriangle(vertexIndex, vertexIndex + vertPerLine + 1, vertexIndex + vertPerLine);
//                data.AddTriangle(vertexIndex + vertPerLine + 1, vertexIndex, vertexIndex + 1);
                vertexIndex++;
            }
        }

        for (int vertex = 0; vertex < data.vertices.Length - 2; vertex += 2)
        {
            data.AddTriangle(vertex, vertex + 2, vertex + 1);
            data.AddTriangle(vertex + 2, vertex, vertex + 1);

            data.AddTriangle(vertex +1, vertex + 2+1, vertex + 1+1);
            data.AddTriangle(vertex + 2+1, vertex+1, vertex + 1+1);
        }

        var mesh = new Mesh();
        mesh.vertices = data.vertices;
        mesh.triangles = data.triangles;
        mesh.uv = data.uvs;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GameObject slope = new GameObject();
        slope.transform.parent = transform;
        MeshFilter meshFilter = slope.AddComponent<MeshFilter>();
        MeshRenderer renderer = slope.AddComponent<MeshRenderer>();
        renderer.material.color = Color.white;
        meshFilter.sharedMesh = mesh;


        for (int x = 0; x < length; x++) // generate bottoms
        {
            PlaceCube(new Vector3(x, minY, 0));
            PlaceCube(new Vector3(x, minY, width));
//            PlaceCube(new Vector3(endX, minY, 0));
//            PlaceCube(new Vector3(endX, minY, width));
        }
    }

    private void PlaceCube(Vector3 vector3)
    {
        GameObject corner = GameObject.CreatePrimitive(PrimitiveType.Cube);
        corner.transform.position = vector3;
        corner.transform.localScale = scale;
        corner.transform.parent = transform;
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
            triangles = new int[width * height * 2 * 3];
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