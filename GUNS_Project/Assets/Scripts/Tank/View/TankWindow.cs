using UnityEngine;

public class TankWindow : EntityWindow
{
    [SerializeField] private float _bulletSpeed;

    public float BulletSpeed => _bulletSpeed;

    public override void Init()
    {
        
    }

    public TankView CreateSolider(TankView prefab, Transform point, TankModel model)
    {
        TankView created = CreatePrefab(prefab, point.position);

        Add(created, model);
        
        return created;
    }
}