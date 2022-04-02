using System.Collections.Generic;
using UnityEngine;

public class AnimalsFactory : MonoBehaviour
{
	public Animal AnimalPrefab;

	private EntityPresetBase _presetBase;

	protected void Awake()
	{
		_presetBase = EntityPresetBase.GetInstance();
	}

	private Animal CreateAnimal(EntityType type)
	{
		Animal newAnimal = Instantiate(AnimalPrefab);
		newAnimal.Type = type;
		newAnimal.Preset = _presetBase[type];
		newAnimal.Vitality = newAnimal.Preset.StartVitality;

		SpriteRenderer spriteRenderer = newAnimal.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = newAnimal.Preset.Sprite;

		CircleCollider2D collider = newAnimal.GetComponent<CircleCollider2D>();
		collider.radius = newAnimal.Preset.CollideRadius;

		return newAnimal;
	}
}