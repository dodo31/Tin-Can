using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionsToolkit
{
	private ContactFilter2D _contactFilter;

	public CollisionsToolkit()
	{
		_contactFilter = new ContactFilter2D()
		{
			layerMask = 1 << LayerMask.NameToLayer("Entities"),
			useLayerMask = true,
			useTriggers = true,
		};
	}

	public List<Entity> GetCloseEntities(Transform transform, Collider2D proximityCollider)
	{
		List<Collider2D> collidingColliders = this.GetCollidingColliders(proximityCollider);
		return this.GetCollidingEntities(transform, collidingColliders);
	}
	public List<Entity> GetHittingEntities(Transform transform, Collider2D hitboxCollider)
	{
		List<Collider2D> collidingColliders = this.GetCollidingColliders(hitboxCollider);
		return this.GetCollidingEntities(transform, collidingColliders);
	}

	private List<Collider2D> GetCollidingColliders(Collider2D sourceCollider)
	{
		List<Collider2D> collidingColliders = new List<Collider2D>();
		sourceCollider.OverlapCollider(_contactFilter, collidingColliders);
		return collidingColliders;
	}

	private List<Entity> GetCollidingEntities(Transform transform, List<Collider2D> collidingColliders)
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

	public ContactFilter2D ContactFilter { get => _contactFilter; }
}