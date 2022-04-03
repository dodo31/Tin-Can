using UnityEngine;

public class AnimalsFactory : EntitiesFactory
{
	public Animal AnimalPrefab;

	public Animal CreateAnimal(EntityType type)
	{
		AnimalPreset animalPreset = (AnimalPreset)_presetBase[type];
		Animal newAnimal = Instantiate(AnimalPrefab);
		
		newAnimal.Type = type;
		newAnimal.Preset = animalPreset;
		newAnimal.Vitality = animalPreset.StartVitality;

		SpriteRenderer spriteRenderer = newAnimal.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = animalPreset.Sprite;

		newAnimal.HitboxCollider.radius = animalPreset.CollideRadius;
		newAnimal.ProximityCollider.radius = animalPreset.ViewDistance;

		return newAnimal;
	}
}