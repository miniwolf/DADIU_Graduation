using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEditor;


public class SlopeGenerator : MonoBehaviour
{
    public AnimationCurve slopeCurve;
    public int length = 100;
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


        int width = 2;

        var data = new MeshData(length, width);

        float minY = float.MaxValue;
        float startX = 0, endX = length;

        { // add plane vertices, uvs and triangles
            int vertexIndex = 0;
            for (int x = 0; x < length; x++)
            {
                float pos = x / (float) length;
                float y = length * slopeCurve.Evaluate(pos);

                if (y < minY)
                    minY = y;

                Vector3 start = new Vector3(x, y, 0);
                {
                    PlaceCube(start);
                    data.vertices[vertexIndex] = start;
                    data.uvs[vertexIndex] = new Vector2(x / (float) length, 0);
                    vertexIndex++;

                    start.z = start.z + width;

                    PlaceCube(start);
                    data.vertices[vertexIndex] = start;
                    data.uvs[vertexIndex] = new Vector2(x / (float) length, 1);
                    vertexIndex++;
                }
            }

            for (int vertex = 0; vertex < data.planeVertices -2; vertex += 2)
            {
                data.AddTriangle(vertex, vertex + 2, vertex + 1);
                data.AddTriangle(vertex + 2, vertex, vertex + 1);

                data.AddTriangle(vertex + 1, vertex + 2 + 1, vertex + 1 + 1);
                data.AddTriangle(vertex + 2 + 1, vertex + 1, vertex + 1 + 1);
            }
        }


        {// add bottom triangles + vertices and UVS
			int vertexIndex = data.planeVertices;
            for (int x = 0; x < length; x++)
            {
                Vector3 start = new Vector3(x, minY, 0);
                PlaceCube(start);
				data.vertices[vertexIndex] = start;
				data.uvs[vertexIndex] = new Vector2(x / (float) length, 0);
                vertexIndex++;

                start = new Vector3(x, minY, width);
                PlaceCube(start);
                data.vertices[vertexIndex] = start;
				data.uvs[vertexIndex] = new Vector2(x / (float) length, 1);
                vertexIndex++;
            }

            for (int vertex = data.planeVertices; vertex < data.planeVertices + data.bottomVertices - 2; vertex += 2)
            {
                data.AddTriangle(vertex, vertex + 2, vertex + 1);
                data.AddTriangle(vertex + 2, vertex, vertex + 1);

                data.AddTriangle(vertex + 1, vertex + 2 + 1, vertex + 1 + 1);
                data.AddTriangle(vertex + 2 + 1, vertex + 1, vertex + 1 + 1);
            }
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
        public int planeTriangles, planeVertices, planeUVs;
        public int bottomTriangles, bottomVertices, bottomUVs;
        public int sideTriangles, sideVertices, sideUVs;


        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;

        int index;

        public MeshData(int length, int width)
        {
            planeTriangles = length * width * 2 * 3;
            planeVertices = length * width;
            planeUVs = length * width;

            bottomTriangles = length * width * 2 * 3;
            bottomVertices = length * width;
            bottomUVs = length * width;

            vertices = new Vector3[planeVertices + bottomVertices];
            uvs = new Vector2[planeUVs + bottomUVs];
            triangles = new int[planeTriangles + bottomTriangles];
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