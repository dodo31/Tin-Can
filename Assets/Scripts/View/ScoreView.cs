using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreView : MonoBehaviour
{
	public TextMeshProUGUI ScoreText;

	private RectTransform _rectTransform;

	protected void Awake()
	{
		_rectTransform = this.GetComponent<RectTransform>();
	}

	public void SetScore(long newScore)
	{
		string scorePlainText = newScore.ToString();
		string scoreFormattedText = string.Empty;

		for (int i = 0; i < scorePlainText.Length; i++)
		{
			int charIndex = scorePlainText.Length - i - 1;
			char currentChar = scorePlainText[charIndex];

			if (i > 0 && i % 3 == 0)
			{
				scoreFormattedText = '.' + scoreFormattedText;
			}

			scoreFormattedText = currentChar + scoreFormattedText;
		}

		ScoreText.text = scoreFormattedText + " km";
	}

	public void SetScoreHeight(float size)
	{
		_rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, size);
	}
}
