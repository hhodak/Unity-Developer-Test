using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private float xOffset = 5.0f;
    private float zOffset = -5.0f;

    void Update()
    {
        transform.position = new Vector3(player.position.x + xOffset, transform.position.y, player.position.z + zOffset);
    }
}
