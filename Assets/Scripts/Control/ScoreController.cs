using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public ScoreView ScoreView;

	public float TimeToScoreFactor;

	protected void Start()
	{
		ScoreView.SetScore(0);
	}

	protected void FixedUpdate()
	{
		long score = (long) (Time.fixedTimeAsDouble * TimeToScoreFactor /** UnityEngine.Random.Range(1f, 1.001f)*/);
		ScoreView.SetScore(score);
	}
}