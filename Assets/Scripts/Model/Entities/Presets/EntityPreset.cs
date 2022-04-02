using System;
using UnityEngine;

[Serializable]
public abstract class EntityPreset
{
	public Sprite Sprite;
	public float CollideRadius;

	public float VitalitySpeed;

	public float StartVitality;
	public float MaxVitality;

	public float ReproductionThreshold;
	public float ReproductionCost;

	public float NutritionalValue;
}