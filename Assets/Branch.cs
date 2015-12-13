using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(Rigidbody2D))]
public class Branch : MonoBehaviour
{
	public Branch parent;
	[Range (0f, 1f)]
	public float startGrowth = 0f;
	[Range (0f, 1f)]
	public float spreadFactor = 0.9f;
	public int numBranches = 2;

	private Rigidbody2D rigidBody;
	private float initialMass;
	
	private List<Branch> branches;

	private float growth;
	public float Growth
	{
		get { return growth; }
		private set
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

		branches = new List<Branch>();
	}

	public void Grow(float amount)
	{
		float currentGrowth = Growth + amount;
//		float overTheTop = Mathf.Min(0f, currentGrowth - spreadFactor);
		Growth = currentGrowth;
	}
}
