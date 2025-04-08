using System.Collections.Generic;
using UnityEngine;

public class BuildingWindow : AbstractWindowUi
{
    [SerializeField] private List<BuildingPoint> _models;

    public List<BuildingPoint> Models => _models;

    public override void Init()
    {
        
    }
}