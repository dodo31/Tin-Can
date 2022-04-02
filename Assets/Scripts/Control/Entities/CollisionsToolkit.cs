using System.Collections.Generic;
using UnityEngine;

public class CollisionsToolkit
{
	public CollisionsToolkit()
	{
		
	}

	public List<Entity> GetCloseEntities(Transform transform, CircleCollider2D proximityCollider)
	{
		Collider2D[] collidingColliders = this.GetCollidingColliders(proximityCollider);
		return this.GetCollidingEntities(transform, collidingColliders);
	}

	public List<Entity> GetHittingEntities(Transform transform, CircleCollider2D hitboxCollider)
	{
		Collider2D[] collidingColliders = this.GetCollidingColliders(hitboxCollider);
		return this.GetCollidingEntities(transform, collidingColliders);
	}

	private Collider2D[] GetCollidingColliders(CircleCollider2D sourceCollider)
	{
		int layerMask = 1 << LayerMask.NameToLayer("Entities");
		return Physics2D.OverlapCircleAll(sourceCollider.transform.position, sourceCollider.radius, layerMask, 0f);
	}

	private List<Entity> GetCollidingEntities(Transform transform, Collider2D[] collidingColliders)
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