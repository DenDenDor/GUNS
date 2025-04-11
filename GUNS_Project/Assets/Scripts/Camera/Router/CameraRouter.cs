using UnityEngine;

public class CameraRouter : IRouter
{
    private CameraWindow CameraWindow => UiController.Instance.GetWindow<CameraWindow>();

    public void Init()
    {
        
        CameraController.Instance.Init(CameraWindow, new CameraMovement(CameraWindow.CurrentCamera.transform, () => EntityController.Instance.Player.transform, 
            () => CameraWindow.Speed, 
            () => CameraWindow.Offset,
            () => CameraWindow.SmoothTime));


        Debug.Log(EntityController.Instance.Player + " PLATYER ");
        CameraWindow.UpdateLookAt(EntityController.Instance.Player.LookAtTransform);

        UpdateController.Instance.Add(OnUpdate);
    }

    private void OnUpdate()
    {
    }

    public void Exit()
    {
        
    }
}