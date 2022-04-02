using UnityEngine;

public class VegetalsFactory : EntitiesFactory
{
	public Vegetal VegetablePrefab;

	public Vegetal CreateVegetable(EntityType type)
	{
		VegetalPreset vegetalPreset = (VegetalPreset)_presetBase[type];
		Vegetal newVegetable = Instantiate(VegetablePrefab);
		
		newVegetable.Type = type;
		newVegetable.Preset = vegetalPreset;
		newVegetable.Vitality = vegetalPreset.StartVitality;

		SpriteRenderer spriteRenderer = newVegetable.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = vegetalPreset.Sprite;

		CircleCollider2D collider = newVegetable.GetComponent<CircleCollider2D>();
		collider.radius = vegetalPreset.CollideRadius;

		return newVegetable;
	}
}