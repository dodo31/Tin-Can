using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityPresetBase
{
	private Dictionary<EntityType, EntityPreset> _presets;

	private EntityPresetBase()
	{
		Dictionary<EntityType, EntityPreset> _presets = new Dictionary<EntityType, EntityPreset>();
	}

	public void CreatePresets()
	{
		_presets = new Dictionary<EntityType, EntityPreset> {
			{
				EntityType.FOX_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Fox 1"),
					CollideRadius = 0.72f,

					ViewDistance = 15,

					VitalitySpeed = -0.1f,
					MoveSpeed = 0.15f,

					StartVitality = 30,
					MaxVitality = 100,

					NutritionalValue = 30,
					Power = 5,

					ReproductionThreshold = 0.5f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.RABBIT_1,
						EntityType.ANIMAL_3,
					},

					Predators = new List<EntityType>()
					{

					},
				}
			},
			{
				EntityType.RABBIT_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Rabbit 1"),
					CollideRadius = 0.43f,

					ViewDistance = 8,

					VitalitySpeed = -0.08f,
					MoveSpeed = 0.08f,

					StartVitality = 35,
					MaxVitality = 100,

					NutritionalValue = 80,
					Power = 6,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					Preys = new List<EntityType>()
					{
						EntityType.ANIMAL_3,
						EntityType.TREE_1,
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1,
					},
				}
			},
			{
				EntityType.ANIMAL_3, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Animal 3"),
					CollideRadius = 2.35f,

					ViewDistance = 12,

					VitalitySpeed = -0.12f,
					MoveSpeed = 0.03f,

					StartVitality = 28,
					MaxVitality = 100,

					NutritionalValue = 100,
					Power = 8,

					ReproductionThreshold = 0.55f,
					ReproductionCost = 35,

					Preys = new List<EntityType>()
					{
						EntityType.TREE_1,
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1,
						EntityType.RABBIT_1,
					},
				}
			},
			{
				EntityType.TREE_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Tree 1"),
					CollideRadius = 1.76f,

					VitalitySpeed = 0.12f,

					StartVitality = 20,
					MaxVitality = 100,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
			{
				EntityType.BUSH_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Bush 1"),
					CollideRadius = 0.7f,

					VitalitySpeed = 0.12f,

					StartVitality = 20,
					MaxVitality = 100,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
		};
	}

	public static EntityPresetBase GetInstance()
	{
		return EntityPresetBaseHolder._instance;
	}

	public EntityPreset this[EntityType type]
	{
		get
		{
			return _presets[type];
		}
		set
		{
			_presets[type] = value;
		}
	}

	private static class EntityPresetBaseHolder
	{
		public static EntityPresetBase _instance = new EntityPresetBase();
	}
}