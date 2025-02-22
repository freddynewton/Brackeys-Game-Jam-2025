using UnityEngine;

public class PukeScript : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private GameObject _bloodVFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_bloodVFX, this.transform.position, Quaternion.identity);

        IDamageable damaged = collision.gameObject.GetComponent<IDamageable>();
        if (damaged != null)
        {
            damaged.TakeDamage(_damage, this.transform.position);
        }

            Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
