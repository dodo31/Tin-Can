using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Entity : MonoBehaviour
{

	public EntityType Type;
	public EntityPreset Preset;

	public float Vitality;

	public CircleCollider2D HitboxCollider;

	private List<Entity> _proximityEntities;

	protected Guid _id;

	protected virtual void Awake()
	{
		_proximityEntities = new List<Entity>();
	}

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{

	}

	public Guid Id { get => _id; set => _id = value; }
}
