using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
	public Transform toFollow;
	public float strength = 10f;

	private float offsetFactor;
	private float distance;

	private GameObject[] keepInside;
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

		keepInside = GameObject.FindGameObjectsWithTag("Branch");
		Bounds bounds = new Bounds();
		foreach (GameObject branch in keepInside)
		{
			Renderer rndr = branch.GetComponent<Renderer>();
			bounds.Encapsulate(rndr.bounds);
		}
		float height = bounds.center.y + bounds.extents.y + 4f;
		Camera.main.orthographicSize = Mathf.Max(5f, height/2);
	}
}
