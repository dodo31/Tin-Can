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
	
	protected Guid _id;

	protected List<Entity> _proximityEntities;

	protected CollisionsToolkit _collisionsToolkit;
	
	public event Action<Entity> OnDeath;

	protected virtual void Awake()
	{
		_proximityEntities = new List<Entity>();
		_collisionsToolkit = new CollisionsToolkit();
	}

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{
		
	}
	
	protected virtual void FixedUpdate()
	{
		Vitality = Mathf.Clamp(Vitality + Preset.VitalitySpeed, 0, Preset.MaxVitality);
	}

	public Guid Id { get => _id; set => _id = value; }
}
