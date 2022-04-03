using System;
using UnityEngine;

public class Egg : Entity
{
	private const float HATCHING_DELAY = 3;

	private float _hatchingTime;

	protected override void Awake()
	{
		_hatchingTime = Time.fixedTime + HATCHING_DELAY;
	}

	protected override void FixedUpdate()
	{
		this.GrowIfRequired();
		
		if(Time.fixedTime >= _hatchingTime)
		{
			this.Die();
		}
	}

	protected override bool IsAlive()
	{
		return false;
	}

	protected override void Die()
	{
		this.PublishDeath();
	}
}