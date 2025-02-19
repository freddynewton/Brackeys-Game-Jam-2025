using System;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _player;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");

        if (_player == null)
        {
            Console.WriteLine("Could not find Player or Player Tag");
        }
    }




}
