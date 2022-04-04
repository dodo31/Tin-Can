using System;
using UnityEngine;
using UnityEngine.UI;

public class KeepGoingMenu : MonoBehaviour
{
	public Button KeepGoingButton;
	public Button ExitButton;

	public event Action OnKeepGoing;
	public event Action OnExit;

	protected void Awake()
	{
		KeepGoingButton.onClick.AddListener(OnKeepGoing.Invoke);
		ExitButton.onClick.AddListener(OnExit.Invoke);
	}

	public void Toggle(bool enable)
	{
		gameObject.SetActive(enable);
	}
}
