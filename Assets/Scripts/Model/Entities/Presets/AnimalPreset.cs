using System;
using System.Collections.Generic;

[Serializable]
public class AnimalPreset : EntityPreset
{
	public float ViewDistance;

	public float MoveSpeed;

	public float ReproductionThreshold;
	public float ReproductionVivality;

	public List<EntityType> Preys = new List<EntityType>();
	public List<EntityType> Predators = new List<EntityType>();
}