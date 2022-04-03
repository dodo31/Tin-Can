using UnityEngine;

public class MainController : MonoBehaviour
{
    public EntitiesController EntitiesController;
    public CameraController CameraController;
    
    protected void Awake()
    {
        EntitiesController.OnHumanMoved += CameraController.FollowPlayer;
    }

    protected void Update()
    {
        
    }
}
