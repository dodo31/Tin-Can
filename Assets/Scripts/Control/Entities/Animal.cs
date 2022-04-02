using System.Collections.Generic;
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

	protected override void Update()
	{
		base.Update();

		List<Entity> closeEntities = this.GetCloseEntities();
		List<Entity> hittingEntities = this.GetHittingEntities();

		if (closeEntities.Count > 0)
		{
			List<Entity> closePreys = this.ExtractClosePreys(closeEntities);
			List<Entity> sameSpecies = this.ExtractCloseSameSpecies(closeEntities);
			List<Entity> closePredators = this.ExtractClosePredators(closeEntities);

			if (closePredators.Count > 0)
			{
				Debug.Log(Type + " Flee");
			}
			else if (sameSpecies.Count > 0)
			{
				Debug.Log(Type + " Reproduce");
			}
			else if (closePreys.Count > 0)
			{
				Debug.Log(Type + " Miam");
			}
			else
			{
				Debug.Log(Type + " Idle");
			}
		}
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

	private List<Entity> ExtractCloseSameSpecies(List<Entity> entities)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		return new List<Entity>(entities.FindAll((entity) => Type == entity.Type));
	}

	private List<Entity> ExtractClosePredators(List<Entity> entities)
	{
		AnimalPreset animalPreset = (AnimalPreset)Preset;
		return new List<Entity>(entities.FindAll((entity) => animalPreset.Predators.Contains(entity.Type)));
	}
}
