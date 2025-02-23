using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    [SerializeField] private Sprite openDoorSprit;
    [SerializeField] private Sprite closedDoorSprite;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private bool isDooActivated;

    // Check every second
    public IEnumerator OpenDoor()
    {
        while (FindAnyObjectByType<EnemyInformation>())
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }

        isDooActivated = true;

        SetDoorActive(true);
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

        StartCoroutine(OpenDoor());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDooActivated)
        {
            // Find All DeathSprites and Particle Systems and Delete them
            // DeathSprites are the sprites with the Name DeathSprite

            var particles = FindObjectsOfType<ParticleSystem>();

            foreach (var particle in particles)
            {
                Destroy(particle.gameObject);
            }

            var deathSprites = FindObjectsOfType<SpriteRenderer>().Where(x => x.name == "DeathSprite");

            foreach (var sprite in deathSprites)
            {
                Destroy(sprite.gameObject);
            }

            GameSceneFlowManager.Instance.LoadScene(sceneToLoad, true, 2f);
            SoundManager.Instance.SetLevelWon();
            isDooActivated = true;
        }
    }
}
