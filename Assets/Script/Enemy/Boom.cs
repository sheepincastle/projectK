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
    void OnCollisionEnter2D(Collision2D other)
    {
        //�÷��̾�� ���˽� ����
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("y");
            GameManager.player_current_HP -= boom_damage;
        }
    }
}
