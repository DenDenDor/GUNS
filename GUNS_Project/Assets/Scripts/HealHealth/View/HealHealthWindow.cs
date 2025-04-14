using UnityEngine;

public class HealHealthWindow : AbstractFactoryWindow
{
    [SerializeField] private Transform _spawnPoint;
    
    public override void Init()
    {
        
    }

    public HealHealthView Create(HealHealthView findPrefab)
    {
       return CreatePrefab(findPrefab, _spawnPoint, true);
    }
}