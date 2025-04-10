using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllyPoint
{
   [SerializeField] private Transform _attackButton;
   [SerializeField] private List<Transform> _moveToPoints;

   public Transform AttackButton => _attackButton;

   public List<Transform> MoveToPoints
   {
      get
      {
         List<Transform> points = new List<Transform>();
            
         points.AddRange(_moveToPoints);
         points.Reverse();
            
         return points;
      }
   }
}
