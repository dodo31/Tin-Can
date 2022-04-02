using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public VegetalsFactory VegetalsFactory;
	public AnimalsFactory AnimalsFactory;

	protected void Start()
	{
		VegetalSpawnButton[] vegetalSpawnButtons = GameObject.FindObjectsOfType<VegetalSpawnButton>();
		AnimalSpawnButton[] animalSpawnButtons = GameObject.FindObjectsOfType<AnimalSpawnButton>();

		foreach (VegetalSpawnButton spawnButton in vegetalSpawnButtons)
		{
			spawnButton.OnClick += SpawnVegetal;
		}

		foreach (AnimalSpawnButton spawnButton in animalSpawnButtons)
		{
			spawnButton.OnClick += SpawnAnimal;
		}
	}

	protected void Update()
	{

	}

	public void SpawnVegetal(EntityType type)
	{
		Vegetal newVegetal = VegetalsFactory.CreateVegetable(type);
		this.SpawnEntity(newVegetal);
	}

	public void SpawnAnimal(EntityType type)
	{
		Animal newAnimal = AnimalsFactory.CreateAnimal(type);
		this.SpawnEntity(newAnimal);
	}

	private void SpawnEntity(Entity newEntity)
	{
		newEntity.transform.SetParent(transform);
		newEntity.transform.position = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
		
		newEntity.OnDeath += this.KillEntity;
	}

	public void KillEntity(Entity entity)
	{
		DestroyImmediate(entity.gameObject);
	}

	private Entity[] GetEntities()
	{
		return this.GetComponentsInChildren<Entity>();
	}
}
