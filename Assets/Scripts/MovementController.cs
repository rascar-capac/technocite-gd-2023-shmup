using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.InputSystem.InputAction;

public class MovementController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _metersPerSecond = 8f;

    private NavMeshAgent _controller;
    private Vector2 _direction;

    // -- METHODS

    //called by the UnityEvent in PlayerInput
    public void UpdateDirection(CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void ApplyMovement()
    {
        _controller.velocity = new Vector3(_direction.x, 0f, _direction.y) * _metersPerSecond;
    }

    // -- UNITY

    private void Awake()
    {
        _controller = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ApplyMovement();
    }
}
