using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SoldierWindow : AbstractWindowUi
{
    [SerializeField] private List<Transform> _spawnsPoints;
    [SerializeField] private List<Transform> _moveToPoints;

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


    public SoldierView CreateSolider(SoldierView soldierView)
    {
        return Instantiate(soldierView, _spawnsPoints.GetRandomRepeatElement().position, quaternion.identity);
    }
}