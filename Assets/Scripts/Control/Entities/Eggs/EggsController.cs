using System.Collections.Generic;
using System;
using UnityEngine;

public class EggsController : MonoBehaviour
{
	public BottomBarController BottomBarController;

	private EggsSequence _eggsSequence;
	private Queue<EntityType> _spwanQueue;

	private GameTime _gameTime;

	public event Action<EntityType> OnEggOdered;

	protected void Awake()
	{
		BottomBarController.OnNewEggButtonClick += this.SpawnNewEgg;

		_eggsSequence = EggsSequence.GetInstance();
		_gameTime = GameTime.GetInstance();

		_spwanQueue = new Queue<EntityType>();

		_eggsSequence.OnEggsAvailable += this.AddEggs;

		BottomBarController.SetNewEggEntityAmount(0);
	}

	public void AddEggs(EntityType eggsType, int eggsCount)
	{
		EntityType? lastType = this.LastEggType();

		if (!lastType.HasValue)
		{
			BottomBarController.SetNewEggEntityPreset(eggsType);
			BottomBarController.SetNewEggEntityAmount(eggsCount);
		}

		for (int i = 0; i < eggsCount; i++)
		{
			_spwanQueue.Enqueue(eggsType);
		}
	}

	protected void FixedUpdate()
	{
		_eggsSequence.RefreshSequence(_gameTime.FixedTimeSinceSceneStart);
	}

	public void SpawnNewEgg()
	{
		if (_spwanQueue.Count > 0)
		{
			EntityType eggType = _spwanQueue.Dequeue();
			OnEggOdered?.Invoke(eggType);

			EntityType? lastType = this.LastEggType();

			if (lastType.HasValue)
			{
				BottomBarController.SetNewEggEntityPreset(lastType.Value);
			}

			int newEggsEntityAmount = this.LastEggsOfSameTypeCount();
			BottomBarController.SetNewEggEntityAmount(newEggsEntityAmount);
		}
	}

	private EntityType? LastEggType()
	{
		if (_spwanQueue.Count > 0)
		{
			return _spwanQueue.Peek();
		}
		else
		{
			return null;
		}
	}

	private int LastEggsOfSameTypeCount()
	{
		if (_spwanQueue.Count > 0)
		{
			int eggsCount = 0;
			EntityType lastType = _spwanQueue.Peek();
			EntityType currentType = lastType;

			IEnumerator<EntityType> itTypes = _spwanQueue.GetEnumerator();
			itTypes.MoveNext();

			do
			{
				currentType = itTypes.Current;

				if (lastType == currentType)
				{
					eggsCount++;
				}
			} while (lastType == currentType && itTypes.MoveNext());

			return eggsCount;
		}
		else
		{
			return 0;
		}
	}
}