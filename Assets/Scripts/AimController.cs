using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class AimController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Transform _muzzle;

    private Camera _camera;
    private Vector2 _mousePosition;

    // -- METHODS

    //called by the UnityEvent in PlayerInput
    public void UpdateMousePosition(CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    // -- UNITY

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 targetPosition = Vector3.zero;
        Plane plane = new Plane(Vector3.up, _muzzle.position);
        Ray ray = _camera.ScreenPointToRay(_mousePosition);

        if(plane.Raycast(ray, out float distance))
        {
            targetPosition = ray.GetPoint(distance);
        }

        transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
    }
}
