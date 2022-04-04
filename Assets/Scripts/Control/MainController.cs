using UnityEngine;

public class MainController : MonoBehaviour
{
	public EntitiesController EntitiesController;
	public CameraController CameraController;

	public BottomBarController BottomBarController;

	public PauseMenu PauseMenu;
	public KeepGoingMenu KeepGoingMenu;

	public long KeepGoingTimeout;

	private bool _hasKeptGoing;

	protected void Awake()
	{
		EntitiesController.OnHumanMoved += CameraController.FollowPlayer;

		EntitiesController.OnEntityKilled += this.DecreaseEntityAmountInUi;
		EntitiesController.OnEntitySpawned += this.IncreaseEntityAmountInUi;

		BottomBarController.OnPauseButtonClick += this.TogglePause;
		BottomBarController.OnQuitButtonClick += Application.Quit;

		PauseMenu.OnResume += this.TogglePause;
		PauseMenu.OnQuit += Application.Quit;

		KeepGoingMenu.OnKeepGoing += this.DisableKeepGoingMenu;
		KeepGoingMenu.OnExit += Application.Quit;

		PauseMenu.Toggle(false);
		KeepGoingMenu.Toggle(false);

		_hasKeptGoing = false;
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

		if (Time.fixedTime >= KeepGoingTimeout && !_hasKeptGoing)
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
}
