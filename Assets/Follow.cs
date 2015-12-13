using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
	public Transform toFollow;
	public float strength = 10f;

	private float offsetFactor;
	private float distance;

	// Use this for initialization
	void Start ()
	{
		float offset = transform.position.y -  toFollow.position.y;
		offsetFactor = offset / Camera.main.orthographicSize;
		distance = transform.position.z - toFollow.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 targetPosition = toFollow.position;
		targetPosition.y += offsetFactor * Camera.main.orthographicSize;
		targetPosition.z += distance;

		Vector3 position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * strength);
		transform.position = position;
	}
}
