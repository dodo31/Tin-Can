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

}
