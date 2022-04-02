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
			spawnButton.OnClick += this.SpawnVegetal;
		}

		foreach (AnimalSpawnButton spawnButton in animalSpawnButtons)
		{
			spawnButton.OnClick += this.SpawnAnimal;
		}
	}

	protected void Update()
	{

	}

	public void SpawnVegetal(EntityType type, Vector3 position)
	{
		Vegetal newVegetal = VegetalsFactory.CreateVegetable(type);
		newVegetal.OnBirth += this.SpawnVegetal;

		this.SpawnEntity(newVegetal, position);
	}

	public void SpawnAnimal(EntityType type, Vector3 position)
	{
		Animal newAnimal = AnimalsFactory.CreateAnimal(type);
		newAnimal.OnBirth += this.SpawnAnimal;

		this.SpawnEntity(newAnimal, position);
	}

	private void SpawnEntity(Entity newEntity, Vector3 position)
	{
		if (transform.childCount < 100)
		{
			newEntity.transform.SetParent(transform);
			newEntity.transform.position = position;
			newEntity.OnDeath += this.KillEntity;
		}
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
