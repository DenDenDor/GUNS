using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletWindow : AbstractWindowUi
{
    private List<BulletView> _bulletViews = new();

    public List<BulletView> BulletViews => _bulletViews.Where(x=>x != null).ToList();

    public override void Init()
    {
        
    }

    public BulletView Create(BulletView prefab, Transform point)
    {
        BulletView bulletView = Object.Instantiate(prefab, point.position, Quaternion.identity);
        
        _bulletViews.Add(bulletView);

        return bulletView;
    }
}