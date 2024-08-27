using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetecPlayer : MonoBehaviour
{
    //플레이어가 공격범위 안에 있는지 확인
    public bool in_area;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            in_area = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            in_area = false;
        }
    }
}
