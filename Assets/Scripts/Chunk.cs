using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    int vertexIndex = 0;
    List<Vector3> verticies = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {

        for (int y = 0; y < Voxel.ChunkHeight; y++)
        {
            for (int x = 0; x < Voxel.ChunkWidth; x++)
            {
                for (int z = 0; z < Voxel.ChunkWidth; x++)
                {
                    addVoxeltoChunk(new Vector3(x, y, z));
                }
            }
        }

        createMesh();
        
    }

    private void addVoxeltoChunk(Vector3 pos)
    {
        for (int p = 0; p < 6; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = Voxel.voxelTris[p, i];
                verticies.Add(Voxel.voxelVertex[triangleIndex] + pos);
                triangles.Add(vertexIndex);
                uvs.Add(Voxel.voxelUvs[i]);

                vertexIndex++;
            }
        }
    }

    private void createMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
