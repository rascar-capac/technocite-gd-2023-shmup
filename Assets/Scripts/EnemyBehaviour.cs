using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // -- FIELDS

    private LifeHolder _player;
    private NavMeshAgent _navMeshAgent;
    private float _damagePercentagePerSecond = 0.1f;

    // -- METHODS

    public void Initialize(LifeHolder player)
    {
        _player = player;
    }

    public void HurtPlayer()
    {
        _player.Hurt(_damagePercentagePerSecond * Time.deltaTime);
    }

    // -- UNITY

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
