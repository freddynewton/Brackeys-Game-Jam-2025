using NUnit.Framework;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField] private Sprite openDoorSprit;
    [SerializeField] private Sprite closedDoorSprite;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private int _currentActiveEnemies;

    private bool isDooActivated;

    public void AddActiveEnemy()
    {
        _currentActiveEnemies++;
    }

    public void RemoveActiveEnemy()
    {
        _currentActiveEnemies--;
        if (_currentActiveEnemies <= 0)
        {
            SetDoorActive(true);
        }
    }

    public void SetDoorActive(bool active)
    {
        spriteRenderer.sprite = active ? openDoorSprit : closedDoorSprite;
        boxCollider.enabled = active;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        SetDoorActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isDooActivated)
        {
            GameSceneFlowManager.Instance.LoadScene(sceneToLoad, true, 2f);
            SoundManager.Instance.SetLevelWon();
            isDooActivated = true;
        }
    }
}
