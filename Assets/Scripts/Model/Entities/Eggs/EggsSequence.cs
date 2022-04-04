using System.Collections.Generic;
using System;
using UnityEngine;

public class EggsSequence
{
	private Queue<EggPool> _sequence;

	public event Action<EntityType, int> OnEggsAvailable;

	private EggsSequence()
	{
		_sequence = EggsSequenceFactory.CreateNewSequence();
	}

	public static EggsSequence GetInstance()
	{
		return EggsSequenceHolder.Instance;
	}

	public void RefreshSequence(float fixedTime)
	{
		int previousSequenceSize = 0;

		do
		{
			previousSequenceSize = _sequence.Count;
			
			if (_sequence.Count > 0)
			{
				EggPool currentPool = _sequence.Peek();
				
				if (fixedTime >= currentPool.Timestamp)
				{
					OnEggsAvailable?.Invoke(currentPool.Type, currentPool.PoolSize);
					_sequence.Dequeue();
				}
			}
		} while (previousSequenceSize != _sequence.Count);
	}

	private static class EggsSequenceHolder
	{
		public static EggsSequence Instance = new EggsSequence();
	}
}