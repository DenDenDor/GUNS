using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
   [SerializeField] private List<Transform> _points;

   public List<Transform> Points => _points;
}
