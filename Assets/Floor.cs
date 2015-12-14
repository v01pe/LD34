using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Branch")
		{
			Expode(other);
		}
	}
	
	private void Expode(Collision2D other)
	{
		other.collider.enabled = false;

		GameObject obj = other.gameObject;
		Joint2D joint = obj.GetComponent<Joint2D>();
		Destroy(joint);

		Rigidbody2D body = obj.GetComponent<Rigidbody2D>();
		Vector2 velocity = body.velocity;
		velocity.y *= -1;
		body.velocity = velocity;
		body.angularVelocity = Random.Range(500f, 1000f) * Mathf.Sign(Random.value-0.5f);

		Branch branch = obj.GetComponent<Branch>();
		branch.parent = null;
	}
}
