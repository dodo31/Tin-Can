using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public Screen MainMenuScreen;

	protected void Awake()
	{
		MainMenuScreen.Toggle(true);
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}
}
