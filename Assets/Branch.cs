using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Branch : MonoBehaviour
{
	[Range (0f, 1f)]
	public float startGrowth = 0f;

	private Rigidbody2D rigidBody;
	private float initialMass;

	private float growth;
	private float Growth
	{
		get { return growth; }
		set
		{
			growth = Mathf.Clamp01(value);
			rigidBody.mass = growth * initialMass;
		}
	}

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		initialMass = rigidBody.mass;
		Growth = startGrowth;
	}

	public void Grow(float amount)
	{
		Growth += amount;
	}
}
