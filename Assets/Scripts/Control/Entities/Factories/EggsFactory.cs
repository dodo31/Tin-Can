using UnityEngine;

public class EggsFactory : EntitiesFactory
{
	public Egg EggPrefab;

	public Egg CreateVegetalEgg(EntityType type)
	{
		Egg newEgg = Instantiate(EggPrefab);
		newEgg.Type = type;
		
		VegetalPreset vegetalPreset = (VegetalPreset)_presetBase[type];

		SpriteRenderer spriteRenderer = newEgg.GetComponent<SpriteRenderer>();
		// spriteRenderer.sprite = animalPreset.EggSprite;
		spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Entities/Eggs/Plain Eggs");

		return newEgg;
	}

	public Egg CreateAnimalEgg(EntityType type)
	{
		Egg newEgg = Instantiate(EggPrefab);
		newEgg.Type = type;

		AnimalPreset animalPreset = (AnimalPreset)_presetBase[type];

		SpriteRenderer spriteRenderer = newEgg.GetComponent<SpriteRenderer>();
		// spriteRenderer.sprite = animalPreset.EggSprite;
		spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Entities/Eggs/Plain Eggs");

		return newEgg;
	}
}