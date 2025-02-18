using UnityEngine;

public class UnitMovementController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private UnitAnimator _unitAnimator;

    private Vector2 _movementInput;

    public Vector2 MovementInput
    {
        get { return _movementInput; }
        private set { _movementInput = value; }
    }

    private void Awake()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _unitAnimator ??= GetComponent<UnitAnimator>();
    }

    public void ApplyInput(Vector2 movementInput)
    {
        _movementInput = movementInput;
        ApplyInput(movementInput.x, movementInput.y);
        _unitAnimator.ApplyInput(movementInput);
    }

    private void ApplyInput(float x, float y)
    {
        Vector2 movement = new Vector2(x, y);
        _rigidbody2D.linearVelocity = movement * _moveSpeed;
    }
}
