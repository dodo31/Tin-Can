using System;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : Screen
{
	public Transform ScorePose;

	public Button ExitButton;

	public event Action OnExit;

	protected void Awake()
	{
		ExitButton.onClick.AddListener(OnExit.Invoke);
	}
}
