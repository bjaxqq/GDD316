using System.Collections;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    private Vector3[,] vertices;
    private int xSize;
    private int zSize;

    [SerializeField] private GameObject streetTile;
    [SerializeField] private GameObject intersectionTile;
    [SerializeField] private GameObject buildingTile;
    [SerializeField] private GameObject treeSelector;

    public void PlaceStreets(Vector3[,] vertices, int xSize, int zSize)
    {
        this.vertices = vertices;
        this.xSize = xSize;
        this.zSize = zSize;
        StartCoroutine("PlaceStreetsCoroutine");
    }

    IEnumerator PlaceStreetsCoroutine()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                if (vertices[x, z].y == -1)
                {
                    Vector3 toDraw = vertices[x, z];
                    toDraw.y = 0.001f;

                    GameObject instance = Instantiate(intersectionTile, toDraw, Quaternion.identity);

                    if (HasXNeighbor(x, z))
                    {
                        instance.transform.Rotate(new Vector3(0, 90, 0));
                    }
                }
            }
            yield return new WaitForSeconds(.00001f);
        }

        StartCoroutine("PlaceBuildingsCoroutine");
    }

    IEnumerator PlaceBuildingsCoroutine()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                Vector3 toDraw = vertices[x, z];
                if (vertices[x, z].y != -1 && vertices[x, z].y != -2)
                {
                    GameObject instance = Instantiate(buildingTile, toDraw, Quaternion.identity);
                    instance.GetComponentInChildren<MeshScaler>().MeshScalerHeight(vertices[x, z].y);
                }
                else if (vertices[x, z].y == -2)
                {
                    if (Random.Range(0f, 1f) > .8)
                    {
                        Instantiate(treeSelector, toDraw, Quaternion.identity);
                    }
                }
            }
            yield return new WaitForSeconds(.00001f);
        }
    }

    private bool HasXNeighbor(int x, int z)
    {
        int upperCheckIndex = x + 1;
        int lowerCheckIndex = x - 1;

        if ((upperCheckIndex < xSize) && vertices[upperCheckIndex, z].y == -1)
        {
            return true;
        }
        else if ((lowerCheckIndex >= 0) && vertices[lowerCheckIndex, z].y == -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
