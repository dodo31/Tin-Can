using UnityEngine;

public class MainController : MonoBehaviour
{
	public EntitiesController EntitiesController;
	public CameraController CameraController;

	public BottomBarController BottomBarController;

	public PauseMenuView PauseMenuView;

	protected void Awake()
	{
		EntitiesController.OnHumanMoved += CameraController.FollowPlayer;

		EntitiesController.OnEntityKilled += this.DecreaseEntityAmountInUi;
		EntitiesController.OnEntitySpawned += this.IncreaseEntityAmountInUi;

		BottomBarController.OnPauseButtonClick += this.TogglePause;
		BottomBarController.OnQuitButtonClick += Application.Quit;

		PauseMenuView.OnResume += this.TogglePause;
		PauseMenuView.OnQuit += Application.Quit;
		
		PauseMenuView.Toggle(false);
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
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			this.TogglePause();
		}
	}

	private void TogglePause()
	{
		bool isPaused = (Time.timeScale == 0);

		if (isPaused)
		{
			PauseMenuView.Toggle(false);
			Time.timeScale = 1;
		}
		else
		{
			PauseMenuView.Toggle(true);
			Time.timeScale = 0;
		}
	}
}
