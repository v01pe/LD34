using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public Balancer balancer;
	public Text tutorial;
	public Image panel;

	private Text text;

	private int maxScore;
	private static int bestScore;
	private static bool first;
	private float score
	{
		get { return maxScore; }
		set
		{
			maxScore = Mathf.Max(maxScore, (int)(value * 1000));
			text.text = "Score: " + maxScore;
			if (!first)
				text.text += "\nBest: " + bestScore;

		}
	}

	private float minTutorialTime;

	void Start()
	{
		StartCoroutine(KeepScore());
		text = GetComponent<Text>();
		maxScore = 0;
		score = 0f;

		minTutorialTime = Time.time + 1.5f;
	}

	public void Done()
	{
		StopAllCoroutines();

		Color tutorialColor = tutorial.color;
		tutorialColor.a = 1f;
		tutorial.color = tutorialColor;

		tutorial.text = "Greed causes the equilibrium to collapse\nAlways maintain reasonability!";

		bestScore = Mathf.Max(bestScore, maxScore);
		first = false;

		StartCoroutine(Restart());
		Invoke("Restart", 5f);
	}

	private IEnumerator Restart()
	{
		yield return new WaitForSeconds(3f);
		float startTime = Time.time;
		float timePassed = 0f;
		while(timePassed < 2f)
		{
			yield return 0;
			timePassed = Time.time - startTime;
			Color color = Color.black;
			color.a = Mathf.Min(1f, timePassed/2f);
			panel.color = color;
		}

		yield return new WaitForSeconds(2f);

		Application.LoadLevel(Application.loadedLevel);
	}
	
	// Update is called once per frame
	private IEnumerator KeepScore()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);
			score = balancer.trunk.GetCummulativeGrowth();

			//HACK AWAY!!
			if (tutorial != null && Time.time > minTutorialTime)
			{
				float overTime = Time.time - minTutorialTime;

				if (score > 0)
				{
					if (tutorial.color.a == 1f)
					{
						minTutorialTime = Time.time;
					}

					Color tutorialColor = tutorial.color;
					tutorialColor.a = Mathf.Max(0f, 1f - overTime/1.5f);
					tutorial.color = tutorialColor;
				}
				else if (overTime > 3f)
				{
					tutorial.text = "Growing will cause everything to totter\nBalance can never be restored, but striven for!";
					minTutorialTime = Time.time;
				}
			}
		}
	}
}
