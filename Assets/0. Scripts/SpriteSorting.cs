using UnityEngine;

public class SpriteSorting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

    private int startingSortingOrder = -50000;

    void Start()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();
        collider2D ??= GetComponent<Collider2D>();
        UpdateSortingOrder();
    }

    private void Update()
    {
        UpdateSortingOrder();
    }

    void UpdateSortingOrder()
    {
        if (spriteRenderer != null)
        {
            Vector3 sortingPosition;

            if (collider2D != null)
            {
                sortingPosition = collider2D.bounds.center;
            }
            else
            {
                sortingPosition = spriteRenderer.bounds.center;
            }

            spriteRenderer.sortingOrder = Mathf.RoundToInt(startingSortingOrder - sortingPosition.y * 100);

            // spriteRenderer.sortingOrder = Mathf.RoundToInt(startingSortingOrder);
        }
    }
}
