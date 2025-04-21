using System;
using UnityEngine;

public class CameraRouter : IRouter
{
    private readonly float _smoothTime = 0.3f;
    
    private GameObject _go;
    private Vector3 _currentVelocity;
    private float _time;

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
        
        UiController.Instance.GetWindow<BombWindow>().Removed += OnRemove;
        UiController.Instance.GetWindow<BombWindow>().Created += OnCreate;
        
        UiController.Instance.GetWindow<PlaneWindow>().Removed += OnRemove;
        UiController.Instance.GetWindow<PlaneWindow>().Created += OnCreate;
    }

    private Transform _point;

    private void OnCreate(Transform obj)
    {
        _point = obj;
    }

    private void OnRemove()
    {
        _point = null;
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

        if (_point != null)
        {
            _go.transform.position = Vector3.SmoothDamp(
                _go.transform.position,
                _point.transform.position,
                ref _currentVelocity,
                _smoothTime
            );

            _time = -0.5f;
        }
        else if (player != null)
        {
            if (_time < 0)
            {
                _go.transform.position = Vector3.MoveTowards(
                    _go.transform.position,
                    player.LookAtTransform.position,
                    6 * Time.deltaTime);

                if (Vector3.Distance(_go.transform.position, player.LookAtTransform.position) < 0.01f)
                {
                    _time = 0;
                }
            }
            else
            {
                _go.transform.position = player.LookAtTransform.position;
            }
        }
    }

    public void Exit()
    {
        
    }
}