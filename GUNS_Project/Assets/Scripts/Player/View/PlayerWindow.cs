using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerWindow : EntityWindow
{
    [SerializeField] private Transform _spawnPoint;
    
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