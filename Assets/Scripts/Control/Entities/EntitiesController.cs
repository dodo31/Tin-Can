using System;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public HumansFactory HumansFactory;
	public VegetalsFactory VegetalsFactory;
	public AnimalsFactory AnimalsFactory;
	public EggsFactory EggsFactory;

	public EggsController EggsController;

	private EntityPresetBase _presetBase;

	public event Action<Vector3> OnHumanMoved;

	public event Action<Entity> OnEntitySpawned;
	public event Action<Entity> OnEntityKilled;

	public event Action OnPlayerKilled;

	protected void Awake()
	{
		VegetalSpawnButton[] vegetalSpawnButtons = GameObject.FindObjectsOfType<VegetalSpawnButton>();
		AnimalSpawnButton[] animalSpawnButtons = GameObject.FindObjectsOfType<AnimalSpawnButton>();

		_presetBase = EntityPresetBase.GetInstance();

		EggsController.OnEggOdered += this.SpawnEgg;
	}

	protected void Start()
	{
		this.SpawnHuman(EntityType.HUMAN_1, new Vector3(0, 0, 0));

		// for (int i = 0; i < 10; i++)
		// {
		// 	this.SpawnAnimal(EntityType.CHIKEN_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		// }

		for (int i = 0; i < 30; i++)
		{
			this.SpawnAnimal(EntityType.CHIKEN_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		}

		for (int i = 0; i < 100; i++)
		{
			this.SpawnAnimal(EntityType.SNAKE_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		}
	}

	protected void FixedUpdate()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			for (int i = 0; i < 30; i++)
			{
				this.SpawnAnimal(EntityType.CHIKEN_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
			}
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			for (int i = 0; i < 10; i++)
			{
				this.SpawnVegetal(EntityType.TREE_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
			}
		}
	}

	protected void Update()
	{

	}

	public void SpawnHuman(EntityType type, Vector3 position)
	{
		Human newHuman = HumansFactory.CreateHuman(type);
		this.SpawnEntity(newHuman, position);
		newHuman.OnMoved += this.OnHumanMoved;
	}
	
	public void SpawnVegetal(EntityType type, Vector3 position)
	{
		Vegetal newVegetal = VegetalsFactory.CreateVegetal(type);
		this.SpawnEntity(newVegetal, position);
		newVegetal.OnBirth += this.SpawnVegetal;
	}
	
	public void SpawnAnimal(EntityType type, Vector3 position)
	{
		Animal newAnimal = AnimalsFactory.CreateAnimal(type);
		this.SpawnEntity(newAnimal, position);
		newAnimal.OnBirth += this.SpawnAnimal;
	}

	public void SpawnEgg(EntityType type, Vector3 position)
	{
		EntityPreset entityPreset = _presetBase[type];

		if (entityPreset is VegetalPreset)
		{
			this.SpawnVegetalEgg(type, position);
		}
		else if (entityPreset is AnimalPreset)
		{
			this.SpawnAnimalEgg(type, position);
		}
	}

	public void SpawnVegetalEgg(EntityType type, Vector3 position)
	{
		Egg newEgg = EggsFactory.CreateVegetalEgg(type);
		this.SpawnEntity(newEgg, position);

		newEgg.OnDeath += (Entity entityToKill) =>
		{
			this.SpawnVegetal(type, position);
		};
	}

	public void SpawnAnimalEgg(EntityType type, Vector3 position)
	{
		Egg newEgg = EggsFactory.CreateAnimalEgg(type);
		this.SpawnEntity(newEgg, position);

		newEgg.OnDeath += (Entity entityToKill) =>
		{
			this.SpawnAnimal(type, position);
		};
	}

	private void SpawnEntity(Entity newEntity, Vector3 position)
	{
		newEntity.transform.SetParent(transform);
		newEntity.transform.position = position;
		newEntity.OnDeath += this.KillEntity;

		OnEntitySpawned?.Invoke(newEntity);
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
		
		if (entityToKill.tag == "Player")
		{
			OnPlayerKilled?.Invoke();
		}

		DestroyImmediate(entityToKill.gameObject);
	}

	private Entity[] GetEntities()
	{
		return this.GetComponentsInChildren<Entity>();
	}
}
