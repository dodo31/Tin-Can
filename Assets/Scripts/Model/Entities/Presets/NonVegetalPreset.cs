using System;
using System.Collections.Generic;

[Serializable]
public abstract class NonVegetalPreset : EntityPreset
{
	public float MoveSpeed;
	
	public float Power;

	public float CollideBounce = 20;
	public float CollideDrag = 12;

	public List<EntityType> Preys = new List<EntityType>();
	public List<EntityType> Predators = new List<EntityType>();

}