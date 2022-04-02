using UnityEngine;

public class EntitiesController : MonoBehaviour
{
	public VegetalsFactory VegatablesFactory;
	public AnimalsFactory AnimalsFactory;

	protected void Start()
	{

	}

	protected void Update()
	{

	}

	public void SpawnVegetable(EntityType type)
	{
        VegatablesFactory.CreateVegetable(type);
	}
    
	public void SpawnAnimal(EntityType type)
	{
        AnimalsFactory.CreateAnimal(type);
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
