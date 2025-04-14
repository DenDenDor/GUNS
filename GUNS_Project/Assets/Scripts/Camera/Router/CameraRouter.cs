using System;
using UnityEngine;

public class CameraRouter : IRouter
{
    private GameObject _go;
    
    private CameraWindow CameraWindow => UiController.Instance.GetWindow<CameraWindow>();

    public void Init()
    {
        _go = new GameObject("LookForPlayer");
        
        CameraController.Instance.Init(CameraWindow, new CameraMovement(CameraWindow.CurrentCamera.transform, GeneratePlayer, 
            () => CameraWindow.Speed, 
            () => CameraWindow.Offset,
            () => CameraWindow.SmoothTime));

        
        WaveController.Instance.StartedNewWave += StartInitCamera;
        
        UpdateController.Instance.Add(OnUpdate);
    }

    private void StartInitCamera()
    {
        CameraWindow.UpdateLookAt(_go.transform);
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
        PlayerView player = EntityController.Instance.Player;
        
        if (player != null)
        {
            _go.transform.position = player.LookAtTransform.position;
        }
    }

    public void Exit()
    {
        
    }
}