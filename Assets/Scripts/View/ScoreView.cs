using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreView : MonoBehaviour
{
	private TextMeshProUGUI _scoreText;

	protected void Awake()
	{
		_scoreText = this.GetComponent<TextMeshProUGUI>();
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

		_scoreText.text = scoreFormattedText + " km";
	}
}
