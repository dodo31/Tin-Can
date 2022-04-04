using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EggPreview : MonoBehaviour
{
	public TMP_Text EggsIncomingText;
	
	public TMP_Text EntitiesCountPrefixText;
	public TMP_Text EntitiesCountText;

	public Image Egg;
	public Image EggDecoration;

	public Image Background;
	public Sprite InactiveBackground;
	public Sprite ActiveBackground;

	public void SetInactive()
	{
		EggsIncomingText.gameObject.SetActive(true);
		EntitiesCountPrefixText.gameObject.SetActive(false);
		EntitiesCountText.gameObject.SetActive(false);

		Background.sprite = InactiveBackground;
		
		Egg.gameObject.SetActive(false);
		
		EggDecoration.gameObject.SetActive(false);
	}

	public void SetActive()
	{
		EggsIncomingText.gameObject.SetActive(false);
		EntitiesCountPrefixText.gameObject.SetActive(true);
		EntitiesCountText.gameObject.SetActive(true);

		Background.sprite = ActiveBackground;

		Egg.gameObject.SetActive(true);
		
		EggDecoration.gameObject.SetActive(true);
	}

	public void SetEgg(EntityPreset preset)
	{
		Egg.sprite = preset.EggSprite;
	}

	public void SetEggAmount(int newAmount)
	{
		EntitiesCountText.text = newAmount.ToString();
	}
}
