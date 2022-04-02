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
			EntityType.ANIMAL_1, new AnimalPreset()
			{
				Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Animal 1"),
				CollideRadius = 2.404756f,

				ViewDistance = 15,
				BaseSpeed = 3,

				StartVitality = 50,
				MaxVitality = 100,

				ReproductionThreshold = 0.5f,
				ReproductionVivality = 20,

				Preys = new List<EntityType>()
				{
					EntityType.ANIMAL_2,
				},

				Predators = new List<EntityType>()
				{
					EntityType.ANIMAL_2,
				},
			}
		},
		{
			EntityType.ANIMAL_2, new AnimalPreset()
			{
				Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Animal 2"),
				CollideRadius = 1.76f,

				ViewDistance = 8,
				BaseSpeed = 1.5f,

				StartVitality = 20,
				MaxVitality = 70,

				ReproductionThreshold = 0.3f,
				ReproductionVivality = 35,

				Preys = new List<EntityType>()
				{
					EntityType.ANIMAL_1,
				},

				Predators = new List<EntityType>()
				{
					EntityType.ANIMAL_3,
				},
			}
		},
		{
			EntityType.ANIMAL_3, new AnimalPreset()
			{
				Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Animal 3"),
				CollideRadius = 2.35f,

				ViewDistance = 12,
				BaseSpeed = 2.5f,

				StartVitality = 35,
				MaxVitality = 85,

				ReproductionThreshold = 0.45f,
				ReproductionVivality = 35,

				Preys = new List<EntityType>()
				{
					EntityType.ANIMAL_2,
				},

				Predators = new List<EntityType>()
				{
					EntityType.ANIMAL_1,
				},
			}
		},
		{
			EntityType.VEGETAL_1, new VegetalPreset()
			{
				Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Vegetal 1"),
				CollideRadius = 1.76f,

				StartVitality = 20,
				MaxVitality = 70,
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