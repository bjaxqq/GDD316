using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    [SerializeField] private int xSize = 40;
    [SerializeField] private int zSize = 40;
    [SerializeField] private float amplitude = 3f;
    [SerializeField] private float scale = .1f;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
        UpdateMesh();
    }

    void GenerateMesh()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * scale, z * scale) * amplitude;
                vertices[i++] = new Vector3(x, y, z);
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0, tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris++] = vert;
                triangles[tris++] = vert + xSize + 1;
                triangles[tris++] = vert + 1;
                triangles[tris++] = vert + 1;
                triangles[tris++] = vert + xSize + 1;
                triangles[tris++] = vert + xSize + 2;
                vert++;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        UpdateMesh();
    }
}
