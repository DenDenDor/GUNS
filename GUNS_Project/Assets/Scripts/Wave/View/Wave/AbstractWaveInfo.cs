using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class AbstractWaveInfo : MonoBehaviour
{
    [SerializeField] private ResourcePoint _resourcePoint;
    [SerializeField] private List<UnblockingBuildingPoint> _points;
    [SerializeField] private EnemyWave _enemyWave;
    [SerializeField] private AllyPoint _allyPoint;
    public ResourcePoint ResourcePoint => _resourcePoint;

    public EnemyWave EnemyWave => _enemyWave;

    public AllyPoint AllyPoint => _allyPoint;
    
    public List<UnblockingBuildingPoint> BuildingPoints => _points;

    public int IdLevel { get; set; }

}
