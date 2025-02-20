using UnityEngine;

public class PlayerAimHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _clampAngle = 60f;

    private UnitAnimator _unitAnimator;

    private void Awake()
    {
        _unitAnimator = GetComponent<UnitAnimator>();
    }

    private void Update()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the direction from the _targetTransform to the mouse position
        Vector3 direction = mousePosition - _targetTransform.position;

        // Get the angle from the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the _targetTransform to face the mouse position with the clamped angle
        _targetTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
