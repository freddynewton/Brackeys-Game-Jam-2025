using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    private ItemHolderLerpPosition itemHolderLerpPosition;

    private void Awake()
    {
        itemHolderLerpPosition ??= GetComponentInChildren<ItemHolderLerpPosition>();


    }
}
