using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public Balancer balancer;

	private Text text;

	private int maxScore;
	private float score
	{
		set
		{
			maxScore = Mathf.Max(maxScore, (int)(value * 1000));
			text.text = "Score: " + maxScore;
		}
	}

	void Start()
	{
		StartCoroutine(KeepScore());
		text = GetComponent<Text>();
		maxScore = 0;
		score = 0f;
	}
	
	// Update is called once per frame
	private IEnumerator KeepScore()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);
			score = balancer.trunk.GetCummulativeGrowth();
		}
	}
}
