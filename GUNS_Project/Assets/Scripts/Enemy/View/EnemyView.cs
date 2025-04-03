using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public void MoveTo(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }
}
