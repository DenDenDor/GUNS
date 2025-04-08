using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletWindow : AbstractWindowUi
{
    public override void Init()
    {
        
    }

    public BulletView Create(BulletView prefab, Transform point)
    {
        BulletView bulletView = Instantiate(prefab, point.position, Quaternion.identity);
        
        return bulletView;
    }
}