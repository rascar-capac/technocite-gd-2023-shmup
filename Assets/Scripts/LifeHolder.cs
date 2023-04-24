using UnityEngine;
using UnityEngine.UI;

public class LifeHolder : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Image _lifeBarImage;
    [SerializeField] private GameManager _gameManager;

    private float _lifeRatio;

    // -- METHODS

    public void Hurt(float damagePercentage)
    {
        ModifyLifeRatio(-damagePercentage);
    }

    private void ModifyLifeRatio(float ratioToAdd)
    {
        _lifeRatio = Mathf.Clamp01(_lifeRatio + ratioToAdd);
        UpdateLifeBar();

        if(_lifeRatio == 0)
        {
            _gameManager.EndGame();
        }
    }

    private void UpdateLifeBar()
    {
        _lifeBarImage.fillAmount = _lifeRatio;
    }

    private void CheckNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.7f);

        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out EnemyBehaviour enemy))
            {
                enemy.HurtPlayer();
            }
        }
    }

    // -- UNITY

    private void Awake()
    {
        _lifeRatio = 1f;
        UpdateLifeBar();
    }

    private void Update()
    {
        CheckNearbyEnemies();
    }
}
