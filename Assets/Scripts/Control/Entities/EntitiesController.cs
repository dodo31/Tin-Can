using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public VegetalsFactory VegetalsFactory;
	public AnimalsFactory AnimalsFactory;

	protected void Start()
	{
		AnimalSpawnButton[] spwnButtons = GameObject.FindObjectsOfType<AnimalSpawnButton>();

		foreach (AnimalSpawnButton spawnButton in spwnButtons)
		{
			spawnButton.OnClick += SpawnAnimal;
		}
	}

	protected void Update()
	{

	}

	public void SpawnVegetable(EntityType type)
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
	}

	public void KillEntity(Entity entity)
	{
		DestroyImmediate(entity);
	}

	private Entity[] GetEntities()
	{
		return this.GetComponentsInChildren<Entity>();
	}
}
