using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnEggsButton : MonoBehaviour
{
	public TMP_Text Text;
	public TMP_FontAsset InactiveFont;
	public TMP_FontAsset ActiveFont;

	public Image Background;

	public Sprite InactiveBackground;
	public Sprite ActiveBackground;

	public void SetInactive()
	{
		Text.font = InactiveFont;
		Text.color = new Color(1, 1, 1, 0.2f);

		Background.sprite = InactiveBackground;
	}

	public void SetActive()
	{
		Text.font = ActiveFont;
		Text.color = Color.white;

		Background.sprite = ActiveBackground;
	}
}
