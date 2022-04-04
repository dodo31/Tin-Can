using UnityEngine;

public class GameTime
{
	private float _timeOffset;

	private GameTime()
	{
		_timeOffset = 0;
	}

	public static GameTime GetInstance()
	{
		return GameTimeHolder.Instance;
	}

	public void SetTimeOffset(float timeOffset)
	{
		_timeOffset = timeOffset;
	}

	public float FixedTimeSinceSceneStart
	{
		get
		{
			return Time.fixedTime - _timeOffset;
		}
	}

	private static class GameTimeHolder
	{
		public static GameTime Instance = new GameTime();
	}
}