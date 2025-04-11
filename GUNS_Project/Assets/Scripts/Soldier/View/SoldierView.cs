using UnityEngine;
using UnityEngine.AI;

public class SoldierView : AbstractEntity
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public override void MoveTo(Vector3 getPosition)
    {
        _navMeshAgent.SetDestination(getPosition);
    }
    
    public void Rotate(Quaternion toRotate)
    {
        _child.transform.rotation = toRotate;
    }
}
