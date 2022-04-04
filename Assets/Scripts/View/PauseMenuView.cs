using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
	public Button ResumeButton;
	public Button QuitButton;

	public event Action OnResume;
	public event Action OnQuit;

	protected void Awake()
	{
		ResumeButton.onClick.AddListener(OnResume.Invoke);
		QuitButton.onClick.AddListener(OnQuit.Invoke);
	}

	public void Toggle(bool enable)
	{
		gameObject.SetActive(enable);
	}
}
