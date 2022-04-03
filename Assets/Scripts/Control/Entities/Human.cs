using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public class Human : Entity
{
	public Weapon Weapon;

	private HumanState _currentState;

	protected override void Awake()
	{
		base.Awake();

		_currentState = HumanState.Idle;
		transform.localScale = Vector3.one;

		Weapon.OnHit += this.HitEntity;
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
		if (Input.GetKey(KeyCode.UpArrow))
		{
			return Vector3.up * HumanPreset.MoveSpeed * Time.timeScale;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			return Vector3.down * HumanPreset.MoveSpeed * Time.timeScale;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			return Vector3.left * HumanPreset.MoveSpeed * Time.timeScale;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			return Vector3.right * HumanPreset.MoveSpeed * Time.timeScale;
		}

		return Vector3.zero;
	}

	protected override void Move(Vector3 delta)
	{
		transform.position += delta;
		transform.localScale = new Vector3(Mathf.Sign(delta.x), 1, 1);
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
			entityToHit.TakeHit(HumanPreset.Power, -hitDirection);
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