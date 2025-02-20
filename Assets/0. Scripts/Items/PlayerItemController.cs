using JetBrains.Annotations;
using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    [SerializeField] private BaseItem _currentItem;

    private ItemHolderLerpPosition itemHolderLerpPosition;

    private void Awake()
    {
        itemHolderLerpPosition ??= GetComponentInChildren<ItemHolderLerpPosition>();

        InputManager.Instance.OnPrimaryInteractPerformed.AddListener(PrimaryItemInteract);
        InputManager.Instance.OnSecondaryInteractPerformed.AddListener(SecondaryItemInteract);
    }

    private void PrimaryItemInteract()
    {
        if (_currentItem == null)
        {
            return;
        }

        _currentItem.PrimaryInteract();
    }

    private void SecondaryItemInteract()
    {
        if (_currentItem == null)
        {
            return;
        }

        _currentItem.SecondaryInteract();
    }
}
