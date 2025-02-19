using UnityEngine;

public class ItemHolderLerpPosition : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _lerpSpeed = 15f;
    [SerializeField] private float _campRange = 0.2f;

    private void Awake()
    {
        // Set the parent transform to null so we can move the item holder freely
        transform.parent = null;
    }

    private void Update()
    {
        // Calculate the direction from the item holder to the target transform
        Vector2 lerpDirection = _targetTransform.position - transform.position.normalized;

        // Lerp the position of the item holder to the target transform
        // the target is our parent transform
        // and clamp the range of the item holder to the target transform
        transform.position = Vector3.Lerp(transform.position, lerpDirection * _campRange, _lerpSpeed * Time.deltaTime);
    }
}
