using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public HumansFactory HumansFactory;
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

		this.SpawnHuman(EntityType.HUMAN_1, new Vector3(0, 0, 0));

		// for (int i = 0; i < 100; i++)
		// {
		// 	this.SpawnAnimal(EntityType.RABBIT_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		// }

		for (int i = 0; i < 30; i++)
		{
			this.SpawnVegetal(EntityType.TREE_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
		}
	}

	protected void Update()
	{

	}

	public void SpawnHuman(EntityType type, Vector3 position)
	{
		Human newHuman = HumansFactory.CreateHuman(type);
		this.SpawnEntity(newHuman, position);
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
		newEntity.transform.SetParent(transform);
		newEntity.transform.position = position;
		newEntity.OnDeath += this.KillEntity;
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

		DestroyImmediate(entityToKill.gameObject);
	}

	private Entity[] GetEntities()
	{
		return this.GetComponentsInChildren<Entity>();
	}
}
