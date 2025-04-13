using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletWindow : AbstractFactoryWindow
{
    public override void Init()
    {
        
    }

    public BulletView Create(BulletView prefab, Transform point)
    {
        BulletView bulletView = CreatePrefab(prefab, point.position);
        
        return bulletView;
    }
}