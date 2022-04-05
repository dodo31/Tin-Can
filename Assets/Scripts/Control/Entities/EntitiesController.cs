using System;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public HumansFactory HumansFactory;
	public VegetalsFactory VegetalsFactory;
	public AnimalsFactory AnimalsFactory;
	public EggsFactory EggsFactory;

	public EggsController EggsController;

	private EntityPresetBase _presetBase;

	private Human _player;

	private List<ParticleSystem> _runningParticleSystems;

	public event Action<Vector3> OnHumanMoved;

	public event Action<Entity> OnEntitySpawned;
	public event Action<Entity> OnEntityKilled;

	public event Action OnPlayerKilled;

	protected void Awake()
	{
		VegetalSpawnButton[] vegetalSpawnButtons = GameObject.FindObjectsOfType<VegetalSpawnButton>();
		AnimalSpawnButton[] animalSpawnButtons = GameObject.FindObjectsOfType<AnimalSpawnButton>();

		_presetBase = EntityPresetBase.GetInstance();
		_player = null;

		_runningParticleSystems = new List<ParticleSystem>();

		EggsController.OnEggOdered += this.SpawnEgg;
	}

	protected void Start()
	{
		_player = this.SpawnHuman(EntityType.HUMAN_1, new Vector3(0f, 0f, 0f));


		/*
		EntityType[] animals = { EntityType.RABBIT_1, EntityType.FOX_1, EntityType.CHIKEN_1, EntityType.COW_1, EntityType.FROG_1 , EntityType.T_REX_1};
		int[] amounts = { 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20};
			for (int j = 0; j < animals.Length; j++)
			{
				for (int i = 0; i < amounts[j]; i++)
				{
					Vector3 randomPos = new Vector3((float)(-sizeX + sizeX*2 * random.NextDouble()), (float)(-sizeY + sizeY * 2 * random.NextDouble()), 0);
					if (Physics2D.OverlapPoint(randomPos, 1 << 6) == null)
						SpawnAnimal(animals[j], randomPos);
				}
			}
		*/
		System.Random random = new System.Random();
		float sizeX = 45;
		float sizeY = 25;
		EntityType[] plants = { EntityType.FLOWER_1, EntityType.GRASS_1, EntityType.TREE_1 };
		int[] amounts2 = { 15, 70, 30, 100, 50, 20 };
		for (int j = 0; j < plants.Length; j++)
		{
			for (int i = 0; i < amounts2[j]; i++)
			{
				Vector3 randomPos = new Vector3((float)(-sizeX + sizeX * 2 * random.NextDouble()), (float)(-sizeY + sizeY * 2 * random.NextDouble()), 0);
				if (Physics2D.OverlapPoint(randomPos, 1 << 6) == null)
					SpawnVegetal(plants[j], randomPos);
			}
		}
	}

	protected void FixedUpdate()
	{
		// if (Input.GetKeyDown(KeyCode.P))
		// {
		//     for (int i = 0; i < 30; i++)
		//     {
		//         this.SpawnAnimal(EntityType.CHIKEN_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		//     }
		// }

		// if (Input.GetKeyDown(KeyCode.T))
		// {
		//     for (int i = 0; i < 10; i++)
		//     {
		//         this.SpawnVegetal(EntityType.TREE_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		//     }
		// }

		this.CleanParticleSystems();
	}

	private void CleanParticleSystems()
	{
		List<ParticleSystem> particleSystemsToRemove = new List<ParticleSystem>();

		foreach (ParticleSystem particleSystem in _runningParticleSystems)
		{
			if (!particleSystem.isPlaying)
			{
				DestroyImmediate(particleSystem.gameObject);
				particleSystemsToRemove.Add(particleSystem);
			}
		}

		foreach (ParticleSystem particleSystem in particleSystemsToRemove)
		{
			_runningParticleSystems.Remove(particleSystem);
		}
	}

	public void SpawnEgg(EntityType type)
	{
		EntityPreset entityPreset = _presetBase[type];
		Vector3 playerPosition = _player.transform.position;

		if (entityPreset is VegetalPreset)
		{
			this.SpawnVegetalEgg(type, playerPosition);
		}
		else if (entityPreset is AnimalPreset)
		{
			this.SpawnAnimalEgg(type, playerPosition);
		}
	}

	public Entity SpawnVegetalEgg(EntityType type, Vector3 position)
	{
		Egg newEgg = EggsFactory.CreateVegetalEgg(type);
		newEgg.OnDeath += (Entity entityToKill) =>
		{
			this.SpawnVegetal(type, position, true);
		};

		return this.SpawnEntity(newEgg, position, false);
	}

	public Entity SpawnAnimalEgg(EntityType type, Vector3 position)
	{
		Egg newEgg = EggsFactory.CreateAnimalEgg(type);
		newEgg.OnDeath += (Entity entityToKill) =>
		{
			this.SpawnAnimal(type, position, true);
		};

		return this.SpawnEntity(newEgg, position, false);
	}

	public Human SpawnHuman(EntityType type, Vector3 position)
	{
		Human newHuman = HumansFactory.CreateHuman(type);
		this.SpawnEntity(newHuman, position, false);
		newHuman.OnMoved += this.OnHumanMoved;

		return newHuman;
	}

	public void SpawnVegetal(EntityType type, Vector3 position)
	{
		this.SpawnVegetal(type, position, false);
	}

	public void SpawnVegetal(EntityType type, Vector3 position, bool fromEgg)
	{
		Vegetal newVegetal = VegetalsFactory.CreateVegetal(type);
		this.SpawnEntity(newVegetal, position, fromEgg);
		newVegetal.OnBirth += this.SpawnVegetal;
	}

	public void SpawnAnimal(EntityType type, Vector3 position)
	{
		this.SpawnAnimal(type, position, false);
	}

	public void SpawnAnimal(EntityType type, Vector3 position, bool fromEgg)
	{
		Animal newAnimal = AnimalsFactory.CreateAnimal(type);
		this.SpawnEntity(newAnimal, position, fromEgg);
		newAnimal.OnBirth += this.SpawnAnimal;
	}

	private Entity SpawnEntity(Entity newEntity, Vector3 position, bool fromEgg)
	{
		newEntity.transform.SetParent(transform);
		newEntity.transform.position = position;
        newEntity.SpawnedFromEgg = fromEgg;
		newEntity.OnDeath += this.KillEntity;

		OnEntitySpawned?.Invoke(newEntity);

		return newEntity;
	}

	public void KillEntity(Entity entityToKill)
	{
		foreach (Entity entity in this.GetEntities())
		{
			if (entity is Animal animal)
			{
				animal.RemoveCloseEntity(entityToKill);
			}
		}

		if (entityToKill.transform == _player.transform)
		{
			OnPlayerKilled?.Invoke();
		}

		// Particles on death
		// ParticleSystem entityParticleSystem = entityToKill.ParticleSystem;

		// if (entityParticleSystem != null)
		// {
		//     entityParticleSystem.transform.SetParent(null);
		//     entityParticleSystem.Play();
		//     _runningParticleSystems.Add(entityParticleSystem);
		// }

		OnEntityKilled?.Invoke(entityToKill);

		DestroyImmediate(entityToKill.gameObject);
	}

	private Entity[] GetEntities()
	{
		return this.GetComponentsInChildren<Entity>();
	}
}
