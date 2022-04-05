using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public ScoreView ScoreView;

	public float ScoreOffset;

	public float TimeToScoreFactor;
	
	private GameTime _gameTime;

	protected void Awake()
	{
		_gameTime = GameTime.GetInstance();
	}

	protected void Start()
	{
		ScoreView.SetScore(0);
	}

	protected void FixedUpdate()
	{
		long score = (long)(ScoreOffset + (_gameTime.FixedTimeSinceSceneStart) * TimeToScoreFactor /** UnityEngine.Random.Range(1f, 1.001f)*/);
		ScoreView.SetScore(score);
	}

	public void MoveViewToDeathPose(Transform targetPose)
	{
		ScoreView.transform.SetParent(targetPose, false);
		ScoreView.SetScoreHeight(55);
	}
}