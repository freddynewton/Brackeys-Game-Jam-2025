using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Settings")]
    [Range(0.01f,10f)][SerializeField] private float _lerpTime = 5f;

    [Header("References")]
    public Transform PlayerPosition;

    void FixedUpdate()
    {
        if (PlayerPosition == null)
        {
            return;
        }

        LerpFollowPlayer();
    }

    public void LerpFollowPlayer()
    {
        Vector3 temp = transform.position;
        temp.x = Mathf.Lerp(transform.position.x, PlayerPosition.position.x, Time.deltaTime * _lerpTime);
        temp.y = Mathf.Lerp(transform.position.y, PlayerPosition.position.y, Time.deltaTime * _lerpTime);
        transform.position = temp;
    }                   
}
