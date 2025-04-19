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
        BulletView bulletView = CreatePrefabByRotation(prefab, point);

        bulletView.transform.rotation = Quaternion.Euler(0, -bulletView.transform.eulerAngles.y, 0);
        
        return bulletView;
    }
}