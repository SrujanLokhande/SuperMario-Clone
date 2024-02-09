using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;               // gets the gameObject with name Player
    }

    private void Update()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, _player.position.x);         // camera does not move to the left;
        transform.position = cameraPosition;
    }
}
