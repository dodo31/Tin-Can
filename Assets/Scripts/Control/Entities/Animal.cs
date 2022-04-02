using System.Runtime.CompilerServices;
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
		
		Debug.Log(hittingEntities.Count);
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
}
