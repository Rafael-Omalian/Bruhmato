using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float yPosition;
    private Transform player;

    void Start()
    {
        yPosition = transform.position.y;
        player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, yPosition, player.position.z);
    }
}
