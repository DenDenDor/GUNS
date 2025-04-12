using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerWindow : EntityWindow
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed = 4;
    public float Speed => _speed;

    public float RotationSpeed => _rotationSpeed;

    public override void Init()
    {
        
    }

    public PlayerView CreatePlayer(PlayerView view, Action<PlayerView> playerView, PlayerModel model)
    {
        PlayerView created = Instantiate(view, _spawnPoint.position, quaternion.identity);

        playerView(created);
        
        Add(created, model);

        return created;
    }
}