using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : AbstractEntity, IRotatableView
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent.updateRotation = false;
    }

    public override void MoveTo(Vector3 getPosition)
    {
        _navMeshAgent.SetDestination(getPosition);
    }

    public void Rotate(Quaternion quaternion)
    {
        _child.transform.rotation = quaternion;
    }
}
