using System;
using UnityEngine;

public class Egg : Entity
{
	private const float HATCHING_ANIMATION_DURATION = 0.3f;
	private const float HATCHING_DELAY = 3;

	private float _hatchingTime;

	private bool _isVibbing;
	private Animator _animator;

	protected override void Awake()
	{
		_hatchingTime = Time.fixedTime + HATCHING_DELAY;

		_isVibbing = false;
		_animator = this.GetComponent<Animator>();
	}
	
	protected override void FixedUpdate()
	{
		this.GrowIfRequired();

		if (Time.fixedTime >= (_hatchingTime - HATCHING_ANIMATION_DURATION * 1.2f) && !_isVibbing)
		{
			_animator.SetTrigger("Vibe");
			_isVibbing = true;
		}

		if (Time.fixedTime >= _hatchingTime)
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