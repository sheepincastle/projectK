using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    
    EnemyMove enemyMove;
    Animator animator;
    EnemyData enemyData;

    GameObject player;
    Transform player_transform;
    Player player_script;

    int bat_power = 3;   
    float attack_cooltime = 1;
    bool attack_able = true;
    float distance;
    


    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_transform = player.GetComponent<Transform>();
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player_transform.position);
        if (distance < 2 && attack_able) // 공격
        {
            animator.SetTrigger("Attack");
            if( PlayerMove.moveable == false)// 상태이상시 추가 데미지
            {
                GameManager.player_current_HP -= bat_power + 2;
                if (enemyData.enemy_current_HP <= enemyData.enemy_HP - 3)
                {
                    enemyData.enemy_current_HP += 3; // 스턴시 체력 회복
                }
            }
            else
            {
                GameManager.player_current_HP -= bat_power;
            }
            attack_able = false;

            Invoke("ToAttackAble", attack_cooltime);
        }
        
        
        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 0.75f);
        }
    }

    void ToAttackAble()
    {
        attack_able = true;
    }

}

