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

		Branch[] keepInside = FindObjectsOfType<Branch>();
		Bounds bounds = new Bounds();
		foreach (Branch branch in keepInside)
		{
			if (branch.parent == null)
				continue;

			Collider2D collider = branch.GetComponent<Collider2D>();
			if (collider != null)
				bounds.Encapsulate(collider.bounds);
		}
		float size = Mathf.Max(bounds.center.y + bounds.extents.y + 4f, (bounds.extents.x + 4f) / Camera.main.aspect );
		Camera.main.orthographicSize = Mathf.Clamp(size/2, 5f, 15f);
	}
}
