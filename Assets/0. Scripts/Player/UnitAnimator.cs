using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [HideInInspector] public bool IsLookingLeft;
    private Animator _animator;

    private int _verticalHash;
    private int _horizontalHash;
    private int _speedHash;

    private bool _isInitialized;

    private bool _isPlayer;

    void Awake()
    {
        _animator ??= GetComponentInChildren<Animator>();
        _horizontalHash = Animator.StringToHash("Horizontal");
        _verticalHash = Animator.StringToHash("Vertical");
        _speedHash = Animator.StringToHash("Speed");
        _isInitialized = true;

        _isPlayer = gameObject.layer == LayerMask.NameToLayer("Player");
    }

    public void ApplyInput(Vector2 input)
    {
        if (!_isInitialized) { return; }

        if (_isPlayer)
        {
            // Get the mouse position in screen coordinates
            Vector3 mouseScreenPosition = Input.mousePosition;

            // Convert the screen position to world position
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            // Set _isLookingLeft based on the comparison between mouse position x and transform.position.x
            IsLookingLeft = mouseWorldPosition.x < transform.position.x;

            // Update the animator's transform scale based on _isLookingLeft
            _animator.transform.localScale = new Vector2(IsLookingLeft ? -1 : 1, 1);
        }
        else
        {
            if (input.x != 0)
            {
                IsLookingLeft = input.x < 0 ? true : false;

                _animator.transform.localScale = new Vector2(IsLookingLeft ? -1 : 1, 1);
            }
        }

        if (input.magnitude > 0)
        {
            _animator.SetFloat(_verticalHash, input.x);
            _animator.SetFloat(_horizontalHash, input.y);
        }

        _animator.SetFloat(_speedHash, input.magnitude);
    }

    public void TurnAround()
    {
        IsLookingLeft = !IsLookingLeft;
        _animator.transform.localScale = new Vector2(IsLookingLeft ? -1 : 1, 1);
    }
}
