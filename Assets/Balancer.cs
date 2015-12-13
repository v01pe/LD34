using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class Balancer : MonoBehaviour
{
	public float strength = 100f;

	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		float balance = Input.GetAxis("Balance");
		balance = Mathf.Sqrt(Mathf.Abs(balance)) * Mathf.Sign(balance);

		Vector2 force = Vector2.zero;
		force.x = balance;
		force *= strength * Time.deltaTime;

		rigidBody.AddForce(force, ForceMode2D.Impulse);
	}
}
