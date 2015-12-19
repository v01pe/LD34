using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
	public Score score;
	public GameObject baseBranch;

	private bool notified = false;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Branch")
		{
			Expode(other);

			if (!notified && other.gameObject == baseBranch)
			{
				score.Invoke("Done", 1.5f);
				notified = true;
			}
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
