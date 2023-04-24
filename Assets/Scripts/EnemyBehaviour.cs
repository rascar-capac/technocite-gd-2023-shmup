using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _damagePercentagePerSecond = 0.1f;
    [SerializeField] private int _bearableHitCount = 1;
    [SerializeField] private int _scoreWhenKilled = 1;

    private LifeHolder _player;
    private NavMeshAgent _navMeshAgent;
    private float _lifeRatio;
    private Renderer _renderer;
    private GameManager _gameManager;

    // -- METHODS

    public void Initialize(LifeHolder player, GameManager gameManager)
    {
        _player = player;
        _gameManager = gameManager;
    }

    public void HurtPlayer()
    {
        _player.Hurt(_damagePercentagePerSecond * Time.deltaTime);
    }

    public void TakeHit()
    {
        _lifeRatio -= 1f / _bearableHitCount;

        if(_lifeRatio <= 0)
        {
            Destroy(gameObject);
            _gameManager.IncreaseScore(_scoreWhenKilled);
        }
        else
        {
            _renderer.material.SetFloat("_Life", _lifeRatio);
        }
    }

    // -- UNITY

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _renderer = GetComponentInChildren<Renderer>();
        _lifeRatio = 1f;
        _renderer.material.SetFloat("_Life", _lifeRatio);
    }

    private void Update()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}
