using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllyPoint
{
   [SerializeField] private Transform _attackButton;
   [SerializeField] private Transform _bombPoint;
   [SerializeField] private Transform _jetPoint;
   
   [SerializeField] private List<Transform> _moveToPoints;
   [SerializeField] private List<Transform> _moveToTanks;

   public Transform AttackButton => _attackButton;

   public Transform BombPoint => _bombPoint;

   public Transform JetPoint => _jetPoint;

   public List<Transform> MoveToTanks => _moveToTanks;

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
