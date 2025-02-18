using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private Animator _animator;


    private int _verticalHash;
    private int _horizontalHash;
    private int _speedHash;

    private bool _isInitialized;
    private bool _isLookingLeft;

    void Awake()
    {
        _animator ??= GetComponentInChildren<Animator>();
        _horizontalHash = Animator.StringToHash("Horizontal");
        _verticalHash = Animator.StringToHash("Vertical");
        _speedHash = Animator.StringToHash("Speed");
        _isInitialized = true;
    }

    public void ApplyInput(Vector2 input)
    {
        if (!_isInitialized) { return; }

        if (input.x != 0)
        {
            _isLookingLeft = input.x < 0 ? true : false;

            _animator.transform.localScale = new Vector2(_isLookingLeft ? -1 : 1, 1);
        }

        if (input.magnitude > 0)
        {
            _animator.SetFloat(_verticalHash, input.x);
            _animator.SetFloat(_horizontalHash, input.y);
        }
        _animator.SetFloat(_speedHash, input.magnitude);

    }


}
