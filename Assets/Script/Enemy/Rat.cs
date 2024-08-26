using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    EnemyMove enemyMove;
    Animator animator;
    EnemyData enemyData;

    GameObject player;
    Transform player_transform;
    Player player_script;

    float normal_cooltime = 1;
    float attack_cooltime = 3;
    float ability_cooltime = 5;
    bool normal_able = true;
    bool attack_able = true;
    bool ability_able = true;
    float distance;
    bool attack_stiffness = false;
    

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
        if(distance < 2 && attack_able)
        {
            animator.SetTrigger("Attack");
            attack_able = false;

            //플레이어가 부딪히면 경직
            attack_stiffness = true;
            Invoke("StiffnessDisable", 0.667f);//모션이 끝난 뒤 부딪히면 경직x

            Invoke("ToAttackAble", attack_cooltime);//쿨타임
        }
        if(distance < 5 && ability_able)
        {
            animator.SetTrigger("Ability");
            ability_able = false;
            enemyMove.moveable = false;

            //플레이어가 쥐를 보고있으면 스턴걸림
            //징그러워서 스턴걸린다는 설정
            if(player_transform.position.x > transform.position.x)
            {
                if(player_transform.localScale.x > 0)
                {
                    player_script.Attacked(1);
                }
            }
            else if(player_transform.position.x < transform.position.x)
            {
                if(player_transform.localScale.x < 0)
                {
                    player_script.Attacked(1);
                }
            }

            Invoke("ToAbilityAble", ability_cooltime);//쿨타임
            Invoke("ToIdle", 0.583f);//스킬 시전동안 움직이지 않음
        }

        if(enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 0.75f);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && normal_able)//플레이어와 충돌하는동안
        {
            GameManager.player_current_HP -= enemyData.enemy_power;//공격
            other.gameObject.GetComponent<Player>().Hited();
            normal_able = false;
            Invoke("ToNormalAble", normal_cooltime);//쿨타임
            if(attack_stiffness)//attack을 사용했을 때 경직
            {
                player_script.Attacked(0);
                attack_stiffness = false;
            }
        }
    }

    void ToNormalAble()
    {
        normal_able = true;
    }

    void ToAttackAble()
    {
        attack_able = true;
    }

    void ToAbilityAble()
    {
        ability_able = true;
    }

    void ToIdle()
    {
        enemyMove.moveable = true;
    }

    void StiffnessDisable()
    {
        attack_stiffness = false;
    }
}
