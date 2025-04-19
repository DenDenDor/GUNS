using UnityEngine;

public class BombWindow : AbstractFactoryWindow
{
    public override void Init()
    {
        
    }

    public BombView Create(BombView prefab, Vector3 transformPosition)
    {
       return CreatePrefab(prefab, transformPosition);
    }
}