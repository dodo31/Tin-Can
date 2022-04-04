using System;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    public HumansFactory HumansFactory;
    public VegetalsFactory VegetalsFactory;
    public AnimalsFactory AnimalsFactory;

    public event Action<Vector3> OnHumanMoved;

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

        float baseSize = 0.5f;

        EntityType[] animals = { EntityType.RABBIT_1, EntityType.FOX_1, EntityType.CHIKEN_1, EntityType.SNAKE_1 };
        int[] amounts = { 40, 10, 20, 10 };
        System.Random random = new System.Random();
        for (int k = 0; k < 4; k++)
            for (int j = 0; j < animals.Length; j++)
            {
                for (int i = 0; i < amounts[j]; i++)
                {
                    Vector3 randomPos = new Vector3((float)(-250.0 + 500.0 * random.NextDouble()) * baseSize, (float)(-120.0 + 240.0 * random.NextDouble()) * baseSize, 0);
                    if(Physics2D.OverlapPoint(randomPos, 1 << 6)==null)
                        SpawnAnimal(animals[j], randomPos) ;
                }
            }

        EntityType[] plants = { EntityType.BUSH_1, EntityType.FLOWER_1, EntityType.GRASS_1, EntityType.TREE_1 };
        int[] amounts2 = { 100, 100, 200, 100 };
        for (int k = 0; k < 3; k++)
            for (int j = 0; j < plants.Length; j++)
            {
                for (int i = 0; i < amounts2[j]; i++)
                {
                    Vector3 randomPos = new Vector3((float)(-250.0 + 500.0 * random.NextDouble()) * baseSize, (float)(-120.0 + 240.0 * random.NextDouble()) * baseSize, 0);
                    if (Physics2D.OverlapPoint(randomPos, 1 << 6) == null)
                        SpawnVegetal(plants[j], randomPos);
                }
            }
        this.SpawnHuman(EntityType.HUMAN_1, new Vector3(0, 0, 0));

        // for (int i = 0; i < 100; i++)
        // {
        // 	this.SpawnAnimal(EntityType.RABBIT_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
        // }

        // for (int i = 0; i < 30; i++)
        // {
        // 	this.SpawnVegetal(EntityType.TREE_1, new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0));
        // }
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
