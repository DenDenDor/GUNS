using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class UnblockingBuildingPoint
{
    [SerializeField] private BuildingPoint _current;
    [SerializeField] private List<UnblockingBuildingPoint> _blockedPoints;

    public BuildingPoint Current => _current;

    public List<UnblockingBuildingPoint> BlockedPoints => _blockedPoints;
}
