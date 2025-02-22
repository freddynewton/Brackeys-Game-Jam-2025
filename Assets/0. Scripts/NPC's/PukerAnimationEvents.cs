using UnityEngine;

public class PukerAnimationEvents : EnemyAnimationEvents
{
    [SerializeField] private GameObject _puke;
    [Range(0.01f,5)][SerializeField] private float _pukeSpawnRange = 0.1f;
    [SerializeField] private float _pukeForce = 100;

    public void Puke()
    {
        Vector2 direction = _damagePosition.position - this.transform.position;
        GameObject puke = Instantiate(_puke, this.transform.position + (Vector3)(direction * _pukeSpawnRange), Quaternion.identity);
        puke.GetComponent<PukeScript>().SetDamage(_enemyDamage);
        puke.GetComponent<Rigidbody2D>().AddForce(direction * _pukeForce);
        
    }


}
