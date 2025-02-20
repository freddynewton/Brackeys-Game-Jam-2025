using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    [Header("Base Item")]
    [SerializeField] protected string _itemName;

    public abstract void PrimaryInteract();

    public abstract void SecondaryInteract();
}
