using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWindow : EntityWindow
{
    [SerializeField] private float _bulletSpeed = 3;

    public float BulletSpeed => _bulletSpeed;

    public override void Init()
    {
        
    }
    
    public EnemyView CreateEnemy(EnemyView view, EntityModel model, Transform point)
    {
        EnemyView enemy = CreatePrefab(view, point.position);

        Add(enemy, model);
        
        return enemy;
    }
}