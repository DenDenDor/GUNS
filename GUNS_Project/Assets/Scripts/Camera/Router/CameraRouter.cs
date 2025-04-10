using UnityEngine;

public class CameraRouter : IRouter
{
    public void Init()
    {
        CameraWindow cameraWindow = UiController.Instance.GetWindow<CameraWindow>();
        
        CameraController.Instance.Init(cameraWindow, new CameraMovement(cameraWindow.Camera.transform, EntityController.Instance.Player.transform, 
            () => cameraWindow.Speed, 
            () => cameraWindow.Offset,
            () => cameraWindow.SmoothTime));
    }

    public void Exit()
    {
        
    }
}