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
	public int numBranches = -1;
	public Vector2 angleRange = new Vector2(10f, 70f);

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

	private int depth = 0;
	public int Depth { get { return depth; } }

	// Use this for initialization
	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		initialMass = rigidBody.mass;
		Growth = startGrowth;

		branches = new List<Branch>();
	}

	public void Grow(float amount)
	{
		float passOn = 0f;
		if (Growth > spreadFactor)
		{
			passOn += amount / 3f;
			amount -= passOn;
		}

		float currentGrowth = Growth + amount;
		passOn += Mathf.Min(0f, currentGrowth - 1f);

		Growth = currentGrowth;

		Spread(passOn);
	}

	private void Spread(float amount)
	{
		if (amount > 0f)
		{
			if (branches.Count == 0)
			{
				int branchCount = (numBranches < 0) ? Random.Range(1, 4) : numBranches;
				for (int i = 0; i < branchCount; i++)
				{
					GameObject branchObject = GameObject.Instantiate(Resources.Load<GameObject>("Branch"));

					float scale = Random.Range(0.6f, 0.8f);
					branchObject.transform.localScale = transform.localScale * scale;
					Vector3 position = transform.TransformPoint(new Vector3(0f, 0.5f + 0.5f*scale, 0f));
					branchObject.transform.position = position;
					float angle = Random.Range(angleRange[0], angleRange[1]) * ((i%2 == 0) ? -1 : 1);
					branchObject.transform.Rotate(0, 0, transform.rotation.eulerAngles.z + angle);

					Branch newBranch = branchObject.GetComponent<Branch>();
					newBranch.parent = this;
					newBranch.depth = depth + 1;
					branches.Add(newBranch);

					HingeJoint2D joint = branchObject.GetComponent<HingeJoint2D>();
					joint.connectedBody = rigidBody;
				}
			}

			foreach (Branch childBranch in branches)
			{
				if (childBranch != null)
					childBranch.Grow(amount);
			}
		}
	}

	public float GetCummulativeGrowth()
	{
		float cummulativeGrowth = (Growth - startGrowth) * transform.localScale.x;
		foreach (Branch branch in branches)
		{
			if (branch != null)
				cummulativeGrowth += branch.GetCummulativeGrowth();
		}

		return cummulativeGrowth;
	}

	public void Break()
	{
		Joint2D joint = GetComponent<Joint2D>();
		Destroy(joint);
		
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		Vector2 velocity = body.velocity;
		velocity.y *= -1;
		body.velocity = velocity;
		body.angularVelocity = Random.Range(500f, 1000f) * Mathf.Sign(Random.value-0.5f);
		
		parent.Break(this);
		parent = null;
	}

	public void Break(Branch child)
	{
		for (int i = 0; i < branches.Count; i++)
		{
			if (branches[i] == child)
			{
				branches[i] = null;
				break;
			}
		}
	}
}
