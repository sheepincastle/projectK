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
        
        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 0.75f);
        }
    }

    void OnCollisionStay2D(Collision2D other)//접촉하는 동안 공격
    {
        if (distance < 2 && attack_able && other.gameObject.tag == "Player") // 공격
        {
            animator.SetTrigger("Attack");
            if( PlayerMove.moveable == false)// 상태이상시 추가 데미지
            {
                GameManager.player_current_HP -= enemyData.enemy_power + 2;
                Debug.Log("attack+");
                if (enemyData.enemy_current_HP <= enemyData.enemy_HP - 3)
                {
                    enemyData.enemy_current_HP += 3; // 스턴시 체력 회복
                }
            }
            else
            {
                GameManager.player_current_HP -= enemyData.enemy_power;
                Debug.Log("attack");
                Debug.Log(GameManager.player_current_HP);
            }
            attack_able = false;

            Invoke("ToAttackAble", attack_cooltime);
        }
    }

    void ToAttackAble()
    {
        attack_able = true;
    }

}

