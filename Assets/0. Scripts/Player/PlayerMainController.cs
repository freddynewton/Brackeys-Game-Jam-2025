using System.Collections;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnitMovementController _unitMovementController;

    private bool _isWalking;

    private void Awake()
    {
        _unitMovementController ??= GetComponent<UnitMovementController>();
    }

    private void Update()
    {
        _unitMovementController.ApplyInput(InputManager.Instance.MoveInput);

        if (_unitMovementController.MovementInput.magnitude > 0.1 && !_isWalking)
        {
            SoundManager.Instance.PlayPlayerFootsteps();
            _isWalking = true;
        }
        else if (_unitMovementController.MovementInput.magnitude < 0.1f && _isWalking)
        {
            SoundManager.Instance.StopPlayerFootsteps();
            _isWalking = false;
        }
    }
}
