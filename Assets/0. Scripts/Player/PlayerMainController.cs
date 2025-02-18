using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnitMovementController _unitMovementController;
    [SerializeField] private UnitAnimator _unitAnimator;


    private void Awake()
    {
        _unitMovementController ??= GetComponent<UnitMovementController>();
        _unitAnimator ??= GetComponent<UnitAnimator>();
    }

    private void Update()
    {
        _unitMovementController.ApplyInput(InputManager.Instance.MoveInput);
        _unitAnimator.ApplyInput(InputManager.Instance.MoveInput);
    }
}
