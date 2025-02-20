using UnityEngine;

public class PlayerAimHandler : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpSpeed = 5f;

    private void Update()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-coordinate is zero for 2D

        // Get the direction from the _targetTransform to the mouse position
        Vector3 direction = mousePosition - _targetTransform.position;

        // Get the angle from the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Lerp the rotation of the _targetTransform to the target rotation
        _targetTransform.rotation = Quaternion.Lerp(_targetTransform.rotation, targetRotation, _lerpSpeed * Time.deltaTime);
    }
}
