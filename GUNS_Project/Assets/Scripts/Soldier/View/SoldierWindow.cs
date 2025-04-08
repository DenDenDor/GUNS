using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoldierWindow : EntityWindow
{
    [SerializeField] private List<Transform> _spawnsPoints;
    [SerializeField] private List<Transform> _moveToPoints;
    [SerializeField] private Transform _soldierAttackButton;

    public Transform SoldierAttackButton => _soldierAttackButton;

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

    public override void Init()
    {
    }


    public SoldierView CreateSolider(SoldierView soldierView, Transform point, SoldierModel model)
    {
        SoldierView created = Instantiate(soldierView, point.position, quaternion.identity);

        Add(created, model);
        
        return created;
    } 
}