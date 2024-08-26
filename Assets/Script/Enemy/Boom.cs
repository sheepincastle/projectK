using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public int boom_damage = 50;
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어와 접촉시 공격
        if (other.gameObject.tag == "Player")
        {
            GameManager.player_current_HP -= boom_damage;
        }
    }
}
