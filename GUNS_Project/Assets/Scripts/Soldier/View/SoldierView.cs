using UnityEngine;
using UnityEngine.AI;

public class SoldierView : AbstractEntity
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public void MoveTo(Vector3 getPosition)
    {
        _navMeshAgent.SetDestination(getPosition);
    }
}
