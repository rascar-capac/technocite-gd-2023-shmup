using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ShootingController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _collisionExplosionPrefab;
    [SerializeField] private TrailRenderer _bulletTrailPrefab;
    [SerializeField] private float _trailDuration = 0.2f;
    [SerializeField] private int _trailSegmentCount = 10;
    [SerializeField] private Transform _fxContainer;
    [SerializeField] private float _shootingPeriod = 0.5f;
    [SerializeField] private GameManager _gameManager;

    private float _nextShootingTime;
    private bool _isShooting;
    private WaitForSeconds _trailWait;

    // -- METHODS

    //called by the UnityEvent in PlayerInput
    public void UpdateShootingState(CallbackContext context)
    {
        _isShooting = context.performed;
    }

    private void CheckShooting()
    {
        if(!_isShooting)
        {
            return;
        }

        if(Time.time < _nextShootingTime)
        {
            return;
        }


        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if(LayerMask.LayerToName(hitObject.layer) == "Destructible")
            {
                Destroy(hitObject);
                _gameManager.IncreaseScore(1);
            }

            _muzzleFlash.Play();
            TrailRenderer bulletTrail = Instantiate(_bulletTrailPrefab, transform.position, Quaternion.identity, _fxContainer);
            StartCoroutine(PopulateTrail(bulletTrail, transform.position, hitInfo.point));
            ParticleSystem collisionExplosion = Instantiate(_collisionExplosionPrefab, hitInfo.point, Quaternion.identity, _fxContainer);
            collisionExplosion.transform.rotation = Quaternion.LookRotation(hitInfo.normal);
        }

        _nextShootingTime = Time.time + _shootingPeriod;
    }

    private IEnumerator PopulateTrail(TrailRenderer trail, Vector3 origin, Vector3 destination)
    {
        Vector3 direction = (destination - origin).normalized;
        float distance = Vector3.Distance(origin, destination);
        float segmentLength = distance / _trailSegmentCount;

        for(int segmentIndex = 1; segmentIndex <= _trailSegmentCount; segmentIndex++)
        {
            yield return _trailWait;
            trail.transform.position = origin + direction * (segmentIndex * segmentLength);
        }
    }

    // -- UNITY

    private void Awake()
    {
        _trailWait = new WaitForSeconds(_trailDuration / _trailSegmentCount);
    }

    private void Update()
    {
        CheckShooting();
    }
}
