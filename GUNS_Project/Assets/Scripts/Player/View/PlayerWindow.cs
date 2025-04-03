using Unity.Mathematics;
using UnityEngine;

public class PlayerWindow : EntityWindow
{
    [SerializeField] private Transform _spawnPoint;
    
    public override void Init()
    {
        
    }

    public PlayerView CreatePlayer(PlayerView view)
    {
        PlayerView created = Instantiate(view, _spawnPoint.position, quaternion.identity);

        Add(created);

        return created;
    }
}