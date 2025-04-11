using System;
using UnityEngine;

public class CameraRouter : IRouter
{
    private CameraWindow CameraWindow => UiController.Instance.GetWindow<CameraWindow>();

    public void Init()
    {
        
        CameraController.Instance.Init(CameraWindow, new CameraMovement(CameraWindow.CurrentCamera.transform, GeneratePlayer, 
            () => CameraWindow.Speed, 
            () => CameraWindow.Offset,
            () => CameraWindow.SmoothTime));


        StartInitCamera();

        WaveController.Instance.StartedNewWave += StartInitCamera;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void StartInitCamera()
    {
        CameraWindow.UpdateLookAt(EntityController.Instance.Player.LookAtTransform);
    }

    private Transform GeneratePlayer()
    {
        PlayerView playerView = EntityController.Instance.Player;

        if (playerView == null)
        {
            return null;
        }
        
        return playerView.transform;
    }

    private void OnUpdate()
    {
    }

    public void Exit()
    {
        
    }
}