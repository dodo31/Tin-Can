using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public EntitiesController EntitiesController;
	public CameraController CameraController;

	public ScoreController ScoreController;

	public BottomBarController BottomBarController;

	public PauseMenu PauseMenu;

	public KeepGoingMenu KeepGoingMenu;
	public long KeepGoingTimeout;

	public DeathScreen DeathScreen;

	private bool _hasKeptGoing;
	
	private GameTime _gameTime;

	protected void Awake()
	{
		EntitiesController.OnHumanMoved += CameraController.FollowPlayer;

		EntitiesController.OnEntityKilled += this.DecreaseEntityAmountInUi;
		EntitiesController.OnEntitySpawned += this.IncreaseEntityAmountInUi;
		EntitiesController.OnPlayerKilled += this.DisplayDeathScreen;

		BottomBarController.OnPauseButtonClick += this.TogglePause;
		BottomBarController.OnQuitButtonClick += this.ExitToMainMenu;

		PauseMenu.OnResume += this.TogglePause;
		PauseMenu.OnQuit += this.ExitToMainMenu;

		KeepGoingMenu.OnKeepGoing += this.DisableKeepGoingMenu;
		KeepGoingMenu.OnExit += this.ExitToMainMenu;

		DeathScreen.OnExit += this.ExitToMainMenu;

		PauseMenu.Toggle(false);
		KeepGoingMenu.Toggle(false);

		_hasKeptGoing = false;
		
		_gameTime = GameTime.GetInstance();
		_gameTime.SetTimeOffset(Time.fixedTime);
	}

	private void DecreaseEntityAmountInUi(Entity entity)
	{
		BottomBarController.OffsetEntityAmout(entity.Type, -1);
	}

	private void IncreaseEntityAmountInUi(Entity entity)
	{
		BottomBarController.OffsetEntityAmout(entity.Type, 1);
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.TogglePause();
		}

		if (_gameTime.FixedTimeSinceSceneStart >= KeepGoingTimeout && !_hasKeptGoing)
		{
			this.EnableKeepGoingMenu();
		}
	}

	private void TogglePause()
	{
		bool isPaused = (Time.timeScale == 0);

		if (isPaused)
		{
			PauseMenu.Toggle(false);
			Time.timeScale = 1;
		}
		else
		{
			PauseMenu.Toggle(true);
			Time.timeScale = 0;
		}
	}

	private void EnableKeepGoingMenu()
	{
		KeepGoingMenu.Toggle(true);
		_hasKeptGoing = true;
		Time.timeScale = 0;
	}

	private void DisableKeepGoingMenu()
	{
		KeepGoingMenu.Toggle(false);
		Time.timeScale = 1;
	}

	private void DisplayDeathScreen()
	{
		DeathScreen.Toggle(true);
		ScoreController.MoveViewToDeathPose(DeathScreen.ScorePose);
		Time.timeScale = 0;
	}

	public void ExitToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
