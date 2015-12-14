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
		Branch branch = obj.GetComponent<Branch>();
		branch.Break();
	}
}
