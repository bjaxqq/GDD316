using UnityEngine;

public class MeshScaler : MonoBehaviour
{
	[SerializeField] private GameObject[] windows;
	[SerializeField] private float windowSeparation;

	private float gainedHeight = 0;

	public void MeshScalerHeight(float x)
	{
		Vector3 scale = transform.localScale;
		scale.y += x;

		Vector3 pos = transform.position;
		pos.y += x / 2;
		transform.localScale = scale;
		transform.position = pos;
		gainedHeight = x;
		PlaceNewWindows();
	}

	void PlaceNewWindows()
	{
		for (float i = windowSeparation; i < gainedHeight * 1.5; i += windowSeparation)
		{
			foreach (GameObject window in windows)
			{
				GameObject newWindow = Instantiate(window, transform.parent);
				Vector3 pos = window.transform.position;
				pos.y += i;
				newWindow.transform.position = pos;
			}
		}
	}
}
