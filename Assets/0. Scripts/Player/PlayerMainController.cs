using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UnitMovementController _unitMovementController;


    private void Awake()
    {
        _unitMovementController ??= GetComponent<UnitMovementController>();
    }

    private void Update()
    {
        _unitMovementController.ApplyInput(InputManager.Instance.MoveInput);
    }
}
