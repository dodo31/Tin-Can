using System;
using UnityEngine;

[Serializable]
public abstract class EntityPreset
{
	public Sprite sprite;
	public float CollideRadius;
	
	public float StartVitality;
	public float MaxVitality;
}