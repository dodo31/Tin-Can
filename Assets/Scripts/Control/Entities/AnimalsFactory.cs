using System.Collections.Generic;
using UnityEngine;

public class AnimalsFactory : MonoBehaviour
{
	public Animal AnimalPrefab;

	public Dictionary<EntityType, AnimalPreset> Presets = new Dictionary<EntityType, AnimalPreset> {
		{ EntityType.ANIMAL_1, new AnimalPreset() },
		{ EntityType.ANIMAL_2, new AnimalPreset() },
	};

	private Animal CreateAnimal(EntityType type)
	{
		Animal newAnimal = Instantiate(AnimalPrefab);
		newAnimal.Type = type;
		newAnimal.Preset = Presets[type];
		newAnimal.Vitality = newAnimal.Preset.StartVitality;

		SpriteRenderer spriteRenderer = newAnimal.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = newAnimal.Preset.sprite;

		return newAnimal;
	}
}