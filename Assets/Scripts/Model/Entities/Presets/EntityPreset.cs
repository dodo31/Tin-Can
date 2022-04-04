using System;
using UnityEngine;

[Serializable]
public abstract class EntityPreset
{
	public Sprite Sprite;
	public Vector2 SpriteHeadCenter;
	public float CollideRadius;

	public int MaxEntities;

	public float VitalitySpeed;

	public float StartVitality;
	public float MaxVitality;

	public float ReproductionThreshold;
	public float ReproductionCost;

	public float NutritionalValue;
}