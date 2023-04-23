using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // -- FIELDS

    private Transform _player;
    private NavMeshAgent _navMeshAgent;

    // -- METHODS

    public void Initialize(Transform player)
    {
        _player = player;
    }

    // -- UNITY

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.position);
    }
}
