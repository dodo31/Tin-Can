using System;
using System.Collections.Generic;

[Serializable]
public class AnimalPreset : EntityPreset
{
	public float ViewDistance;

	public float MoveSpeed;

	public float Power;

	public float ReproductionThreshold;
	public float ReproductionVivality;

	public float CollideBounce = 20;
	public float CollideDrag = 12;

	public List<EntityType> Preys = new List<EntityType>();
	public List<EntityType> Predators = new List<EntityType>();
}