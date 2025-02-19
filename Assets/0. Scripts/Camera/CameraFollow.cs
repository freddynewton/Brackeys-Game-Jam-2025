using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Settings")]
    [Range(0.01f,10f)][SerializeField] private float _lerpTime = 5f;
    private float _timePassed;
 
    [Header("References")]
    [SerializeField] private Transform _playerPosition;

    void Start()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;

        if (_playerPosition == null)
        {
            Console.WriteLine("Could not find Player or Player Tag");
        }
    }

    void FixedUpdate()
    {
        LerpFollowPlayer();
    }

    public void LerpFollowPlayer()
    {
        Vector3 temp = transform.position;
        temp.x = Mathf.Lerp(transform.position.x, _playerPosition.position.x, Time.deltaTime * _lerpTime);
        temp.y = Mathf.Lerp(transform.position.y, _playerPosition.position.y, Time.deltaTime * _lerpTime);
        transform.position = temp;
    }                   
}
