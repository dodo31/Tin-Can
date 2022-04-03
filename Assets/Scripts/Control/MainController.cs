using UnityEngine;

public class MainController : MonoBehaviour
{
	public EntitiesController EntitiesController;
	public CameraController CameraController;

	public BottomBarController BottomBarController;

	protected void Awake()
	{
		// EntitiesController.OnHumanMoved += CameraController.FollowPlayer;

		EntitiesController.OnEntityKilled += this.DecreaseEntityAmountInUi;
		EntitiesController.OnEntitySpawned += this.IncreaseEntityAmountInUi;
	}

	private void DecreaseEntityAmountInUi(Entity entity)
	{
		BottomBarController.OffsetEntityAmout(entity.Type, -1);
	}

	private void IncreaseEntityAmountInUi(Entity entity)
	{
		BottomBarController.OffsetEntityAmout(entity.Type, 1);
	}
}
