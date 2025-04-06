using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWindow : EntityWindow
{
    [SerializeField] private EnemyWave _enemyWave;

    public EnemyWave EnemyWave => _enemyWave;

    public IEnumerable<Transform> Points => EnemyWave.EnemySpawnPoints.SelectMany(x => x.Points);
    
    public override void Init()
    {
        
    }
    
    public EnemyView CreateEnemy(EnemyView view, EntityModel model, Transform point)
    {
        EnemyView enemy = Instantiate(view, point.position, Quaternion.identity);

        Add(enemy, model);
        
        return enemy;
    }
}