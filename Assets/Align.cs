using UnityEngine;
using System.Collections;

public class Align : MonoBehaviour
{
	public Transform target;

	private Vector3 originalPosition;
	// Use this for initialization
	void Start ()
	{
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		Vector3 position = originalPosition;
		position.x = target.position.x;
		transform.position = position;
	}
}
