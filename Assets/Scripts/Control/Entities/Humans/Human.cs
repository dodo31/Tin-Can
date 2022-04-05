using System.Collections.Generic;
using UnityEngine;
using System;

public class Human : Entity
{
	public Weapon Weapon;

	private HumanState _currentState;

	public event Action<Vector3> OnMoved;

	protected override void Awake()
	{
		base.Awake();

		_currentState = HumanState.Idle;
		transform.localScale = Vector3.one;

		Weapon.OnHit += this.HitEntity;
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		Weapon.SetSortingOrder(_mainSprite.sortingOrder);
	}

	protected override void Update()
	{
		base.Update();

		Vector3 motion = this.GetPlayerMotion();

		if (motion.sqrMagnitude > 0)
		{
			this.Move(motion);
		}

		this.Attack();
	}

	protected Vector3 GetPlayerMotion()
	{
		Vector3 totalMotion = Vector3.zero;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			totalMotion += Vector3.up * HumanPreset.MoveSpeed * Time.timeScale * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			totalMotion += Vector3.down * HumanPreset.MoveSpeed * Time.timeScale * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			totalMotion += Vector3.left * HumanPreset.MoveSpeed * Time.timeScale * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			totalMotion += Vector3.right * HumanPreset.MoveSpeed * Time.timeScale * Time.deltaTime;
		}

		return totalMotion;
	}

	protected override void Move(Vector3 delta)
	{
		transform.position += delta;
		transform.localScale = new Vector3(Mathf.Sign(delta.x), 1, 1);
		_lifeBarView.transform.localScale = new Vector3(Mathf.Sign(delta.x), _lifeBarView.transform.localScale.y, 1);

		OnMoved?.Invoke(transform.position);
	}

	private void Attack()
	{
		if (Weapon.IsHitting())
		{
			List<Entity> hitEntities = _collisionsToolkit.GetHittingEntities(transform, HitboxCollider);

			foreach (Entity hitEntity in hitEntities)
			{
				this.HitEntity(hitEntity);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Weapon.Attack();
		}
	}

	private void HitEntity(Entity entityToHit)
	{
		if (entityToHit.CanTakeHit() && entityToHit != this)
		{
			Vector3 hitDirection = (entityToHit.transform.position - transform.position).normalized;
			float effectiveDamage = Math.Min(HumanPreset.Power, entityToHit.Vitality);
			entityToHit.TakeHit(HumanPreset.Power, -hitDirection);

			float vitalityGain = entityToHit.Preset.NutritionalValue * (effectiveDamage / entityToHit.Preset.MaxVitality) / 2f;
			this.OffsetVitality(Math.Min(vitalityGain, HumanPreset.MaxVitality));
		}
	}

	protected override bool IsAlive()
	{
		return _currentState != HumanState.Dead;
	}

	protected override void Die()
	{
		this.PublishDeath();
	}

	private HumanPreset HumanPreset
	{
		get
		{
			return (HumanPreset)Preset;
		}
	}
}