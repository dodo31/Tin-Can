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
				// ===== HUMANS =====
				EntityType.HUMAN_1, new HumanPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Humans/Human 1"),
					CollideRadius = 0.34f,

					VitalitySpeed = -0.001f,
					MoveSpeed = 0.02f,

					StartVitality = 500,
					MaxVitality = 500,

					NutritionalValue = 30,
					Power = 50,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
					},

					Predators = new List<EntityType>()
					{
					},
				}
			},

			// ===== ANIMALS =====
			{
				EntityType.CHIKEN_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Chiken 1"),
					SpriteHeadCenter = new Vector2(-1, -2),
					CollideRadius = 0.36f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 48,
					MaxVitality = 80,

					NutritionalValue = 30,
					Power = 10,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.TERMITE_1, EntityType.SNAKE_1
					},

					Predators = new List<EntityType>()
					{
						/*EntityType.HUMAN_1,*/ EntityType.FOX_1, EntityType.T_REX_1
					},
				}
			},
			{
				EntityType.COW_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Cow 1"),
					SpriteHeadCenter = new Vector2(-19, -7),
					CollideRadius = 0.88f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 210,
					MaxVitality = 350,

					NutritionalValue = 30,
					Power = 40,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.FLOWER_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.T_REX_1
					},
				}
			},
			{
				EntityType.DIPLODOCUS_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Diplodocus 1"),
					SpriteHeadCenter = new Vector2(-18, -32),
					CollideRadius = 0.83f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 360,
					MaxVitality = 600,

					NutritionalValue = 30,
					Power = 100,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.TREE_1, EntityType.BUSH_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.T_REX_1
					},
				}
			},
			{
				EntityType.FOX_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Fox 1"),
					SpriteHeadCenter = new Vector2(-12, -6),
					CollideRadius = 0.64f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 72,
					MaxVitality = 120,

					NutritionalValue = 30,
					Power = 30,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.TAPIR_1, EntityType.CHIKEN_1, EntityType.RABBIT_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.T_REX_1,
					},
				}
			},
			{
				EntityType.FROG_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Frog 1"),
					SpriteHeadCenter = new Vector2(-2, -1),
					CollideRadius = 0.42f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 24,
					MaxVitality = 40,

					NutritionalValue = 30,
					Power = 40,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.MOSQUITO_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1
					},
				}
			},
			{
				EntityType.GOAT_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Goat 1"),
					SpriteHeadCenter = new Vector2(-12, -6),
					CollideRadius = 0.74f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 90,
					MaxVitality = 150,

					NutritionalValue = 30,
					Power = 30,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.FLOWER_1, EntityType.BUSH_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.T_REX_1
					},
				}
			},
			{
				EntityType.RABBIT_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Rabbit 1"),
					SpriteHeadCenter = new Vector2(-1, -1),
					CollideRadius = 0.49f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 48,
					MaxVitality = 80,

					NutritionalValue = 30,
					Power = 30,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.FLOWER_1, EntityType.GRASS_1
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1, EntityType.T_REX_1,
					},
				}
			},
			{
				EntityType.SNAKE_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Snake 1"),
					SpriteHeadCenter = new Vector2(-10, -5),
					CollideRadius = 0.73f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 48,
					MaxVitality = 80,

					NutritionalValue = 30,
					Power = 50,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.RABBIT_1, EntityType.FOX_1, EntityType.TAPIR_1
					},

					Predators = new List<EntityType>()
					{
						// EntityType.CHIKEN_1
					},
				}
			},
			{
				EntityType.T_REX_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/T-Rex 1"),
					SpriteHeadCenter = new Vector2(-19, -23),
					CollideRadius = 1.19f,

					ViewDistance = 15,

					VitalitySpeed = -0.01f,
					MoveSpeed = 0.15f,

					StartVitality = 240,
					MaxVitality = 400,

					NutritionalValue = 30,
					Power = 120,

					ReproductionThreshold = 0.7f,
					ReproductionCost = 38,

					Preys = new List<EntityType>()
					{
						EntityType.DIPLODOCUS_1, EntityType.GOAT_1, EntityType.FOX_1, EntityType.TAPIR_1, EntityType.COW_1
					},

					Predators = new List<EntityType>()
					{

					},
				}
			},
			{
				EntityType.TAPIR_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Tapir 1"),
					SpriteHeadCenter = new Vector2(-11, -4),

					CollideRadius = 0.69f,

					ViewDistance = 8,

					VitalitySpeed = -0.008f,
					MoveSpeed = 0.08f,

					StartVitality = 72,
					MaxVitality = 120,

					NutritionalValue = 80,
					Power = 20,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					Preys = new List<EntityType>()
					{
						EntityType.TREE_1,
						EntityType.BUSH_1,
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1,
						// EntityType.HUMAN_1
					},
				}
			},
			{
				EntityType.TERMITE_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Termite 1"),
					SpriteHeadCenter = new Vector2(0, 0),
					CollideRadius = 0.26f,

					ViewDistance = 12,

					VitalitySpeed = -0.012f,
					MoveSpeed = 0.03f,

					StartVitality = 6,
					MaxVitality = 10,

					NutritionalValue = 100,
					Power = 10,

					ReproductionThreshold = 0.55f,
					ReproductionCost = 35,

					Preys = new List<EntityType>()
					{
						EntityType.TREE_1,
						EntityType.BUSH_1,
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1,
						EntityType.RABBIT_1,
						// EntityType.HUMAN_1
					},
				}
			},
			{
				EntityType.BEAR_1, new AnimalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Animals/Bear 1"),
					SpriteHeadCenter = new Vector2(-19, -7),
					CollideRadius = 0.26f,

					ViewDistance = 12,

					VitalitySpeed = -0.012f,
					MoveSpeed = 0.03f,

					StartVitality = 150,
					MaxVitality = 250,

					NutritionalValue = 100,
					Power = 80,

					ReproductionThreshold = 0.55f,
					ReproductionCost = 35,

					Preys = new List<EntityType>()
					{
						EntityType.TREE_1,
						EntityType.BUSH_1,
					},

					Predators = new List<EntityType>()
					{
						EntityType.FOX_1,
						EntityType.RABBIT_1,
						// EntityType.HUMAN_1
					},
				}
			},

			// ===== VEGETALS =====
			{
				EntityType.BEE_HIVE_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Bee Hive 1"),
					SpriteHeadCenter = new Vector2(-1, -5),

					CollideRadius = 0.4f,

					VitalitySpeed = 0.02f,

					StartVitality = 40,
					MaxVitality = 80,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
			{
				EntityType.BUSH_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Bush 1"),
					SpriteHeadCenter = new Vector2(0, -1),

					CollideRadius = 0.67f,

					VitalitySpeed = 0.02f,

					StartVitality = 60,
					MaxVitality = 120,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
			{
				EntityType.FLOWER_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Flower 1"),
					SpriteHeadCenter = new Vector2(-1, -6),

					CollideRadius = 0.49f,

					VitalitySpeed = 0.02f,

					StartVitality = 30,
					MaxVitality = 60,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
			{
				EntityType.MOSQUITO_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Mosquito 1"),
					SpriteHeadCenter = new Vector2(0, -2),

					CollideRadius = 0.7f,

					VitalitySpeed = 0.02f,

					StartVitality = 40,
					MaxVitality = 80,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 50,
				}
			},
			{
				EntityType.TREE_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Tree 1"),
					SpriteHeadCenter = new Vector2(-1, -6),

					CollideRadius = 0.41f,

					VitalitySpeed = 0.02f,

					StartVitality = 125,
					MaxVitality = 250,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 400,
				}
			},
			{
				EntityType.GRASS_1, new VegetalPreset()
				{
					Sprite = Resources.Load<Sprite>("Sprites/Entities/Vegetals/Flower 1"),
					SpriteHeadCenter = new Vector2(-1, -6),

					CollideRadius = 0.41f,

					VitalitySpeed = 0.02f,

					StartVitality = 20,
					MaxVitality = 40,

					ReproductionThreshold = 0.6f,
					ReproductionCost = 40,

					NutritionalValue = 60,
				}
			}

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