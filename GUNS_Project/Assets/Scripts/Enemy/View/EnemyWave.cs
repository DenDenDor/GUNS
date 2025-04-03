using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyWave 
{
   [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints;

   public List<EnemySpawnPoint> EnemySpawnPoints => _enemySpawnPoints;
}
