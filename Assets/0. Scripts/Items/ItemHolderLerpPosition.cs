using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class ItemHolderLerpPosition : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _playerGraphics;
    [SerializeField] private float _lerpSpeed = 15f;
    [SerializeField] private float _campRange = 0.2f;

    private void Awake()
    {
        // Set the parent transform to null so we can move the item holder freely
        transform.parent = null;

        _targetTransform.DOLocalMoveY(_targetTransform.localPosition.y + 0.05f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBack);
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            return;
        }

        // Calculate the direction from the item holder to the target transform
        Vector3 lerpDirection = _targetTransform.position - transform.position;

        // Lerp the position of the item holder to the target transform on the x-axis only
        Vector3 newPosition = _targetTransform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, _targetTransform.position.x, _lerpSpeed * Time.deltaTime);
        transform.position = newPosition;

        if (Mathf.Abs(lerpDirection.x) >= _campRange)
        {
            newPosition.x = _targetTransform.position.x - (Mathf.Sign(lerpDirection.x) * _campRange);
            transform.position = newPosition;
        }

        transform.localScale = new Vector3(1, _playerGraphics.localScale.x);
    }
}
