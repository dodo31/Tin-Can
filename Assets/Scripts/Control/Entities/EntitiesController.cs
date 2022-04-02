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
		newVegetal.transform.SetParent(transform);
	}

	public void SpawnAnimal(EntityType type)
	{
		Animal newAnimal = AnimalsFactory.CreateAnimal(type);
		newAnimal.transform.SetParent(transform);
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
