using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityInfoGroupView : MonoBehaviour
{
	public EntityType type;
	public EntityPreset preset;

	[SerializeField]
	private Image image;

	[SerializeField]
	public Image frame;

	[SerializeField]
	private TextMeshProUGUI textMeshProEntityAmount;
	private bool hasBeenEnabled;

	private int entityAmount;

	protected void Awake()
	{
		frame.enabled = false;
	}

	public void UpdateFromPreset(EntityPreset preset)
	{
		this.preset = preset;

		hasBeenEnabled = false;

		image.sprite = preset.Sprite;

		RectTransform rectTransform = (RectTransform)image.transform;
		rectTransform.sizeDelta = new Vector2(preset.Sprite.rect.width * 1.5f, preset.Sprite.rect.height * 1.5f);
		image.gameObject.transform.localPosition = preset.SpriteHeadCenter;
	}

	public void UpdateEntityAmount(int amount)
	{
		entityAmount = amount;

		if (amount > 0)
		{
			hasBeenEnabled = true;
			frame.enabled = true;
		}

		textMeshProEntityAmount.text = amount > 0 ? amount.ToString() : string.Empty;

		image.enabled = hasBeenEnabled;
		image.color = amount > 0 ? Color.white : Color.black;
	}

	public int EntityAmount { get => entityAmount; }
}
