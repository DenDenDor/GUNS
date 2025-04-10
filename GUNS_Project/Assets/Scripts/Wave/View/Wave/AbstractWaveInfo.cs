using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class AbstractWaveInfo : MonoBehaviour
{
    [SerializeField] private Transform _resourcePoint;
    [SerializeField] private List<UnblockingBuildingPoint> _points;
    [SerializeField] private EnemyWave _enemyWave;
    [SerializeField] private AllyPoint _allyPoint;
    public Transform ResourcePoint => _resourcePoint;

    public EnemyWave EnemyWave => _enemyWave;

    public AllyPoint AllyPoint => _allyPoint;
    
    public List<UnblockingBuildingPoint> BuildingPoints => _points;

}
