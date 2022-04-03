using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	public Vector2 Bounds;
	public float MarginFactor;

	private Camera _camera;

	protected void Awake()
	{
		_camera = this.GetComponent<Camera>();
	}

	public void FollowPlayer(Vector3 playerPosition)
	{
		float cameraSize = _camera.orthographicSize;

		Vector3 playerDelta = transform.position - playerPosition;

		float cameraWidth = cameraSize * _camera.aspect;
		float cameraHeight = cameraSize;

		float marginX = cameraWidth * MarginFactor;
		float marginY = cameraHeight * MarginFactor;

		float deltaFromRight = Mathf.Max(-playerDelta.x - (cameraWidth - marginX), 0);
		float deltaFromLeft = -Mathf.Max(playerDelta.x - (cameraWidth - marginX), 0);

		float deltaFromTop = Mathf.Max(-playerDelta.y - (cameraHeight - marginY), 0);
		float deltaFromBottom = -Mathf.Max(playerDelta.y - (cameraHeight - marginY), 0);

		float playerOffsetX = (deltaFromRight != 0 ? deltaFromRight : deltaFromLeft);
		float playerOffsetY = (deltaFromTop != 0 ? deltaFromTop : deltaFromBottom);

		if (playerOffsetX != 0 || playerOffsetY != 0)
		{
			float posX = Mathf.Clamp(transform.position.x + playerOffsetX, -Bounds.x + cameraWidth, Bounds.x - cameraWidth);
			float posY = Mathf.Clamp(transform.position.y + playerOffsetY, -Bounds.y + cameraHeight, Bounds.y - cameraHeight);
			transform.position = new Vector3(posX, posY, transform.position.z);
		}
	}
}
