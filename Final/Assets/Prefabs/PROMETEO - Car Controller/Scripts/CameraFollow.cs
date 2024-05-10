using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform carTransform;
	[Range(1, 10)]
	public float followSpeed = 2;
	[Range(1, 10)]
	public float lookSpeed = 5;
	[Range(1, 100)]
	public float zoomSpeed = 50; // Adjust zoom speed
	Vector3 initialCameraPosition;
	Vector3 initialCarPosition;
	Vector3 absoluteInitCameraPosition;

	void Start()
	{
		initialCameraPosition = gameObject.transform.position;
		initialCarPosition = carTransform.position;
		// Adjust the initial camera position to be closer to the car
		absoluteInitCameraPosition = initialCameraPosition - initialCarPosition + new Vector3(20, 0, 0); // Adjust the z value for initial zoom level
	}

	void FixedUpdate()
	{
		// Look at car
		Vector3 lookDirection = (carTransform.position - transform.position).normalized;
		Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);

		// Move to car with zoom
		float zoomInput = Input.GetAxis("Mouse ScrollWheel"); // Get input for zooming
		absoluteInitCameraPosition.z -= zoomInput * zoomSpeed; // Adjust z position based on input
		Vector3 targetPos = absoluteInitCameraPosition + carTransform.position;
		transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
	}
}
