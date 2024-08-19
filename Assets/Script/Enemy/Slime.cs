using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    EnemyMove enemyMove;
    Animator animator;
    EnemyData enemyData;

    GameObject player;
    Transform player_transform;
    Player player_script;

    float attack_cooltime = 1;
    bool attack_able = true;
    float skill_cooltime = 8;
    bool skill_able = true;
    float distance;



    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_transform = player.GetComponent<Transform>();
        player_script = player.GetComponent<Player>();
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player_transform.position);

        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 1);
        }
    }

    void OnCollisionStay2D(Collision2D other)//접촉하는 동안 공격
    {
        if (distance < 2 && skill_able && other.gameObject.tag == "Player") // 스킬
        {
            animator.SetTrigger("Ability");
            GameManager.player_current_HP -= 2* enemyData.enemy_power;
            player_script.Attacked(1); //스턴
            GameManager.player_power /= 2; // 공격력 감소
            Debug.Log("skill");
            Debug.Log(GameManager.player_current_HP);
            Debug.Log(GameManager.player_power);
            
            skill_able = false;

            Invoke("ToSkillAble", skill_cooltime);
            Invoke("WeakeningDisable", 3);

        }
        else if (distance < 2 && attack_able && other.gameObject.tag == "Player") //공격
        {
            animator.SetTrigger("Attack");
            GameManager.player_current_HP -= enemyData.enemy_power;
            Debug.Log("attack");
            Debug.Log(GameManager.player_current_HP);

            attack_able = false;

            Invoke("ToAttackAble", attack_cooltime);
        }
    }

    void ToAttackAble()
    {
        attack_able = true;
    }

    void ToSkillAble()
    {
        skill_able = true;
    }

    void WeakeningDisable()
    {
        GameManager.player_power *= 2;
        Debug.Log(GameManager.player_power);
    }
}
