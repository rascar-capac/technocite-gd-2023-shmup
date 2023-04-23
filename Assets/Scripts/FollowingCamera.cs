using UnityEngine;

[ExecuteInEditMode]
public class FollowingCamera : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Transform _player;
    [SerializeField] private float _height = 20f;
    [SerializeField] private float _pitchAngle = 60f;

    // -- METHODS

    private void UpdateCameraPosition()
    {
        float zOffset = _height / Mathf.Tan(Mathf.Deg2Rad * _pitchAngle); //trigonometry: tangent A = opposite / adjacent => adjacent = opposite / tangent
        transform.position = new Vector3(_player.position.x, _height, _player.position.z - zOffset);
        transform.eulerAngles = new Vector3(_pitchAngle, 0f, 0f);
    }

    // -- UNITY

    private void Awake()
    {
        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        UpdateCameraPosition();
    }

    private void OnValidate()
    {
        UpdateCameraPosition();
    }
}
