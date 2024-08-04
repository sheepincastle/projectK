using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //칼이나 화살이 적에세 닿으면 공격력만큼 적의 체력 감소
            EnemyData enemyData = other.GetComponent<EnemyData>();
            enemyData.enemy_current_HP -= GameManager.player_power;
        }
    }
}
