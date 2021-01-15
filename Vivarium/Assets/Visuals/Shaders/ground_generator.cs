using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ground_generator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xsize = 1000;
    public int zsize = 1000;
    public List<List<int>> waterCoords;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        waterCoords = this.GetComponent<GetMapCoords>().GetWaterCoords();
        Debug.Log("NUMBER OF WATER: " + waterCoords.Count);
        CreateShape();
        UpdateMesh();
    }

    public bool isWater(int inputX, int inputZ)
    {
        for (var num = 0; num< waterCoords.Count; num+=1)
        {
            if (inputX == waterCoords[num][0] && inputZ == waterCoords[num][1])
            {
                return true;
            }
        }
        return false;
    }

    public void CreateShape()
    {
        int i = 0;
        vertices = new Vector3[(xsize + 1) * (zsize + 1)];
        for (int z = 0; z <= zsize; z++)
        {
            for (int x = 0; x <= xsize; x++)
            {
                Debug.Log(waterCoords[0][0]);
                float y = 0;
                if (isWater(x, z))
                {
                    Debug.Log("YES YES YES");
                    //y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * -5f;
                    y = 0f;
                }
                else
                {
                    //y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 1f;
                    y = 0;
                }
                y += 1.1f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xsize * zsize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zsize; z++)
        {
            for (int x = 0; x < xsize; x++)
            {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert + xsize + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert + xsize + 1;
                    triangles[tris + 5] = vert + xsize + 2;
                    vert++;
                    tris += 6;
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

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
