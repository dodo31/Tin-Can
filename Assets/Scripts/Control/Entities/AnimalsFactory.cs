using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsFactory : MonoBehaviour
{
	public Animal AnimalPrefab;

	public Dictionary<EntityType, AnimalPreset> Presets = new Dictionary<EntityType, AnimalPreset> {
		{ EntityType.ANIMAL_1, new AnimalPreset() },
		{ EntityType.ANIMAL_2, new AnimalPreset() },
	};

	public GameObject CreateAnimal(EntityType type)
	{
		switch (type)
		{
		case EntityType.ANIMAL_1:
			return this.CreateAnimal1();
		case EntityType.ANIMAL_2:
			return this.CreateAnimal2();
		default:
			return null;
		}
	}

	public GameObject CreateAnimal1()
	{
		return this.InstantiateAnimal(EntityType.ANIMAL_1);
	}

	public GameObject CreateAnimal2()
	{
		return this.InstantiateAnimal(EntityType.ANIMAL_2);
	}

	private GameObject InstantiateAnimal(EntityType type)
	{
		AnimalPreset animalPreset = Presets[type];

		Animal animal = Instantiate(AnimalPrefab);
		
		

		return null;
	}
}