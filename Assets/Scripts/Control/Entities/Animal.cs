using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Animal : Entity
{
	public CircleCollider2D ProximityCollider;

	private AnimalState _currentState;

	protected override void Awake()
	{
		base.Awake();

		_currentState = AnimalState.Idle;
	}

	protected override void Start()
	{
		base.Start();

		Rigidbody2D rigidBody = this.GetComponent<Rigidbody2D>();
		rigidBody.drag = AnimalPreset.CollideDrag;
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();

		if (this.IsAlive())
		{
			this.ManageNormalLife();
		}
	}

	private void ManageNormalLife()
	{
		List<Entity> closeEntities = _collisionsToolkit.GetCloseEntities(transform, ProximityCollider);
		List<Entity> hittingEntities = _collisionsToolkit.GetHittingEntities(transform, HitboxCollider);

		if (closeEntities.Count > 0)
		{
			List<Entity> closePredators = this.ExtractClosePredators(closeEntities);
			List<Entity> closeFellows = this.ExtractCloseFellows(closeEntities);
			List<Entity> closePreys = this.ExtractClosePreys(closeEntities);

			if (closePredators.Count > 0)
			{
				this.Flee(closePredators);
			}
			else if (closeFellows.Count > 0 && this.HasEnoughVitalityToReproduce())
			{
				this.Reproduce(closeFellows);
			}
			else if (closePreys.Count > 0)
			{
				this.Eat(closePreys);
			}
		}
		else
		{
			this.Idle();
		}
	}

	private void Flee(List<Entity> closePredators)
	{
		Entity closestPredator = this.ExtractClosestEntity(closePredators);
		Vector3 feeDelta = -(closestPredator.transform.position - transform.position);
		this.MoveToward(feeDelta);

		_currentState = AnimalState.Fleeing;
	}

	private void Reproduce(List<Entity> closeFellows)
	{
		Entity closestFellow = this.ExtractClosestEntity(closeFellows);
		Vector3 reproduceDelta = (closestFellow.transform.position - transform.position);
		this.MoveToward(reproduceDelta);

		_currentState = AnimalState.Reproducing;
	}

	private void Eat(List<Entity> closePreys)
	{
		Entity closestPrey = this.ExtractClosestEntity(closePreys);
		Vector3 eatDelta = (closestPrey.transform.position - transform.position);
		this.MoveToward(eatDelta);

		if (HitboxCollider.IsTouching(closestPrey.HitboxCollider) && closestPrey.CanTakeHit())
		{
			Vector3 preyDelta = (closestPrey.transform.position - transform.position);
			bool isDead = closestPrey.TakeHit(AnimalPreset.Power, -preyDelta.normalized);

			if (isDead)
			{
				Vitality = Math.Min(Vitality + closestPrey.Preset.NutritionalValue, AnimalPreset.MaxVitality);
			}
		}

		_currentState = AnimalState.Eating;
	}

	public override bool TakeHit(float damage, Vector3 hitDirection)
	{
		bool isDead = base.TakeHit(damage, hitDirection);

		if (!isDead)
		{
			Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
			rigidbody.velocity = -hitDirection * AnimalPreset.CollideBounce;
			return isDead;
		}
		else
		{
			return true;
		}
	}

	public override void Die()
	{
		_currentState = AnimalState.Dead;
		this.PublishDeath();
	}

	private void MoveToward(Vector3 delta)
	{
		Vector3 direction = delta.normalized;
		float speed = Math.Min(delta.magnitude, AnimalPreset.MoveSpeed);

		transform.position += direction * speed;
	}

	private void Idle()
	{
		_currentState = AnimalState.Idle;
	}

	private List<Entity> ExtractClosePreys(List<Entity> entities)
	{
		return new List<Entity>(entities.FindAll((entity) => AnimalPreset.Preys.Contains(entity.Type)));
	}

	private List<Entity> ExtractCloseFellows(List<Entity> entities)
	{
		return new List<Entity>(entities.FindAll((entity) => Type == entity.Type));
	}

	private List<Entity> ExtractClosePredators(List<Entity> entities)
	{
		return new List<Entity>(entities.FindAll((entity) => AnimalPreset.Predators.Contains(entity.Type)));
	}

	private Entity ExtractClosestEntity(List<Entity> entities)
	{
		return entities.OrderBy((entity) => Vector3.Distance(transform.position, entity.transform.position)).First();
	}

	private bool HasEnoughVitalityToReproduce()
	{
		return Vitality >= AnimalPreset.ReproductionThreshold * AnimalPreset.MaxVitality;
	}

	private Boolean IsAlive()
	{
		return _currentState != AnimalState.Dead;
	}

	private AnimalPreset AnimalPreset
	{
		get
		{
			return (AnimalPreset)Preset;
		}
	}
}
