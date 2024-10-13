using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform player;

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = player.position;
        pos.x = playerPos.x + 4;
        transform.position = pos;
    }
}
