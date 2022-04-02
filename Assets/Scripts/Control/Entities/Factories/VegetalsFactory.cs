using UnityEngine;

public class VegetalsFactory : EntitiesFactory
{
	public Vegetal VegetablePrefab;

	public Vegetal CreateVegetable(EntityType type)
	{
		Vegetal newVegetable = Instantiate(VegetablePrefab);
		newVegetable.Type = type;
		newVegetable.Preset = _presetBase[type];
		newVegetable.Vitality = newVegetable.Preset.StartVitality;

		SpriteRenderer spriteRenderer = newVegetable.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = newVegetable.Preset.Sprite;

		CircleCollider2D collider = newVegetable.GetComponent<CircleCollider2D>();
		collider.radius = newVegetable.Preset.CollideRadius;

		return newVegetable;
	}
}