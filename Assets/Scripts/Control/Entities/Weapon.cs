using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
	private Animator _animator;

	private Collider2D _collider;

	private CollisionsToolkit _collisionsToolkit;

	public event Action<Entity> OnHit;

	protected void Awake()
	{
		_animator = this.GetComponent<Animator>();
		_collider = this.GetComponentInChildren<Collider2D>();

		_collisionsToolkit = new CollisionsToolkit();
	}

	protected void Update()
	{
		if (this.IsHitting())
		{
			List<Collider2D> collidingColliders = new List<Collider2D>();
			_collider.OverlapCollider(_collisionsToolkit.ContactFilter, collidingColliders);

			List<Entity> collidingEntities = _collisionsToolkit.GetHittingEntities(transform, _collider);

			foreach (Entity collidingEntity in collidingEntities)
			{
				OnHit?.Invoke(collidingEntity);
			}
		}
	}

	public bool IsHitting()
	{
		AnimatorStateInfo currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		return currentStateInfo.IsName("Weapon Hit Animation");
	}

	public void Attack()
	{
		_animator.SetTrigger("Hit");
	}
}