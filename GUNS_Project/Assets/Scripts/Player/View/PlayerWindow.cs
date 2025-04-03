using Unity.Mathematics;
using UnityEngine;

public class PlayerWindow : AbstractWindowUi
{
    [SerializeField] private Transform _spawnPoint;
    
    public override void Init()
    {
        
    }

    public PlayerView CreatePlayer(PlayerView view)
    {
        return Instantiate(view, _spawnPoint.position, quaternion.identity);
    }
}