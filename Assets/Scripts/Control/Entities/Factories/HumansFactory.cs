using UnityEngine;

public class HumansFactory : EntitiesFactory
{
	public Human HumanPrefab;

	public Human CreateHuman(EntityType type)
	{
		HumanPreset animalPreset = (HumanPreset)_presetBase[type];
		Human newHuman = Instantiate(HumanPrefab);
		
		newHuman.Type = type;
		newHuman.Preset = animalPreset;
		newHuman.Vitality = animalPreset.StartVitality;

		SpriteRenderer spriteRenderer = newHuman.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = animalPreset.Sprite;

		newHuman.HitboxCollider.radius = animalPreset.CollideRadius;

		return newHuman;
	}
}