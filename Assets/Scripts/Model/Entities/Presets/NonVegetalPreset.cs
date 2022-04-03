using System;
using System.Collections.Generic;

[Serializable]
public abstract class NonVegetalPreset : EntityPreset
{
	public float MoveSpeed;
	
	public float Power;

	public float CollideBounce = 1.2f;
	public float CollideDrag = 0.4f;

	public List<EntityType> Preys = new List<EntityType>();
	public List<EntityType> Predators = new List<EntityType>();

}