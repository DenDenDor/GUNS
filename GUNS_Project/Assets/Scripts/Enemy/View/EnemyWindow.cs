using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWindow : AbstractWindowUi
{
    [SerializeField] private EnemyWave _enemyWave;

    public EnemyWave EnemyWave => _enemyWave;

    public IEnumerable<Transform> Points => EnemyWave.EnemySpawnPoints.SelectMany(x => x.Points);
    
    public override void Init()
    {
        
    }
    
    public EnemyView CreateEnemy(EnemyView view, Transform point)
    {
        return Instantiate(view, point.position, Quaternion.identity);
    }
}