using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //플레이어의 위치 확인
        Vector3 follow = new Vector3( player.transform.position.x, player.transform.position.y+1, transform.position.z);
        //카메라가 따라감
        transform.position = follow;
    }
}
