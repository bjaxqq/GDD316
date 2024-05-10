using System.Collections;
using UnityEngine;

public class Grid : MonoBehaviour
{
	Mesh mesh;
	Vector3[,] vertices;

	[SerializeField] private int xSize = 40;
	[SerializeField] private int zSize = 40;
	[SerializeField] private float scale;
	[SerializeField] private float amplitude;
	[SerializeField] private int perlinSeed;

	void Start()
	{
		mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;
		CreateGrid();
	}

	void CreateGrid()
	{
		vertices = new Vector3[xSize, zSize];
		Random.InitState(perlinSeed);

		for (int x = 0; x < xSize; x++)
		{
			for (int z = 0; z < zSize; z++)
			{
				float y = Mathf.PerlinNoise(x * scale, z * scale) * amplitude;
				vertices[x, z] = new Vector3(x, y, z);
			}
		}

		StartCoroutine("PlaceObjectsFromGrid");
	}

	IEnumerator PlaceObjectsFromGrid()
	{
		for (int x = 0; x < xSize; x++)
		{
			for (int z = 0; z < zSize; z++)
			{
				Vector3 toDraw = vertices[x, z];
				if (vertices[x, z].y == -1)
				{
					// Draw street tile
					toDraw.y = 0.001f;
					GameObject streetTile = Instantiate(streetTilePrefab, toDraw, Quaternion.identity);

					if (HasXNeighbor(x, z))
					{
						streetTile.transform.Rotate(new Vector3(0, 90, 0));
					}
				}
				else if (vertices[x, z].y != -2)
				{
					// Draw building
					GameObject building = Instantiate(buildingPrefab, toDraw, Quaternion.identity);
					building.GetComponentInChildren<MeshScaler>().MeshScalerHeight(vertices[x, z].y);
				}
				else
				{
					// Spawn a tree sometimes
					if (Random.Range(0f, 1f) > .8)
					{
						Instantiate(treePrefab, toDraw, Quaternion.identity);
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
