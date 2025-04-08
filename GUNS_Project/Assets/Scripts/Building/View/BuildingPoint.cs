using System;
using UnityEngine;

[Serializable]
public class BuildingPoint
{
    [SerializeField] private Transform _point;
    [SerializeField] private BuildingType _type;

    public Transform Point => _point;

    public BuildingType Type => _type;
}
