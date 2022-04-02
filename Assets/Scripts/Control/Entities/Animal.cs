using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Animal : Entity
{
	public CircleCollider2D ProximityCollider;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void FixedUpdate()
	{
		base.Update();

		List<Entity> closeEntities = this.GetCloseEntities();
		List<Entity> hittingEntities = this.GetHittingEntities();

		if (closeEntities.Count > 0)
		{
			List<Entity> closePredators = this.ExtractClosePredators(closeEntities);
			List<Entity> closeFellows = this.ExtractCloseFellows(closeEntities);
			List<Entity> closePreys = this.ExtractClosePreys(closeEntities);

			if (closePredators.Count > 0)
			{
				this.Flee(closePredators);
			}
			else if (closeFellows.Count > 0)
			{
				this.Reproduce(closeFellows);
			}
			else if (closePreys.Count > 0)
			{
				this.Eat(closePreys);
			}
			else
			{
				this.Idle();
			}
		}
	}

	private void Flee(List<Entity> closePredators)
	{
		Entity closestPredator = this.ExtractClosestEntity(closePredators);
		Vector3 feeDelta = -(closestPredator.transform.position - transform.position);
		this.MoveToward(feeDelta);
	}

	private void Reproduce(List<Entity> closeFellows)
	{
		Entity closestFellow = this.ExtractClosestEntity(closeFellows);
		Vector3 reproduceDelta = (closestFellow.transform.position - transform.position);
		this.MoveToward(reproduceDelta);
	}

	private void Eat(List<Entity> closePreys)
	{
		Entity closestPrey = this.ExtractClosestEntity(closePreys);
		Vector3 eatDelta = (closestPrey.transform.position - transform.position);
		this.MoveToward(eatDelta);
	}

	private void MoveToward(Vector3 delta)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		
		Vector3 direction = delta.normalized;
		float speed = Math.Min(delta.magnitude, animalPreset.BaseSpeed);
		
		transform.position += direction * speed;
	}

	private void Idle()
	{

	}

	private List<Entity> GetCloseEntities()
	{
		List<Collider2D> collidingColliders = this.GetCollidingColliders(ProximityCollider);
		return this.GetCollidingEntities(collidingColliders);
	}

	private List<Entity> GetHittingEntities()
	{
		List<Collider2D> collidingColliders = this.GetCollidingColliders(HitboxCollider);
		return this.GetCollidingEntities(collidingColliders);
	}

	private List<Collider2D> GetCollidingColliders(Collider2D sourceCollider)
	{
		List<Collider2D> collidingColliders = new List<Collider2D>();
		ContactFilter2D contactFilter = new ContactFilter2D()
		{
			layerMask = LayerMask.NameToLayer("Entities")
		};

		sourceCollider.OverlapCollider(contactFilter, collidingColliders);

		return collidingColliders;
	}

	private List<Entity> GetCollidingEntities(List<Collider2D> collidingColliders)
	{
		List<Entity> hittingEntities = new List<Entity>();

		foreach (Collider2D collidingCollider in collidingColliders)
		{
			if (collidingCollider.transform != transform)
			{
				Entity collidingEntity = collidingCollider.GetComponent<Entity>();

				if (collidingEntity.HitboxCollider == collidingCollider)
				{
					hittingEntities.Add(collidingEntity);
				}
			}
		}

		return hittingEntities;
	}

	private List<Entity> ExtractClosePreys(List<Entity> entities)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		return new List<Entity>(entities.FindAll((entity) => animalPreset.Preys.Contains(entity.Type)));
	}

	private List<Entity> ExtractCloseFellows(List<Entity> entities)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		return new List<Entity>(entities.FindAll((entity) => Type == entity.Type));
	}

	private List<Entity> ExtractClosePredators(List<Entity> entities)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		return new List<Entity>(entities.FindAll((entity) => animalPreset.Predators.Contains(entity.Type)));
	}

	private Entity ExtractClosestEntity(List<Entity> entities)
	{
		return entities.OrderBy((entity) => Vector3.Distance(transform.position, entity.transform.position)).First();
	}
}
