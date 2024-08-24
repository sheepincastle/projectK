using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{

    EnemyMove enemyMove;
    Animator animator;
    EnemyData enemyData;

    GameObject player;
    Transform player_transform;
    Player player_script;
    Transform transform;

    public int dash_power = 40;
    float dash_speed;
    float distance;
    float dash_cooltime = 0;
    bool dash = false;
    private Vector3 offset = new Vector3(0, 1, 0);
    private Vector3 dash_target;
    void Start()
    {
        transform = GetComponent<Transform>();
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
        //돌진 쿨타임 갱신
        dash_cooltime += Time.deltaTime;
        //죽음
        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 1);
        }
        //일정거리에 플레이어가 있고 돌진쿨타임이 설정값 이상일때 돌진 준비
        if(dash_cooltime >= 10 && distance >= 6 && distance <= 12)
        {
            //잠시 정지
            enemyMove.moveable = false;
            //타겟 위치 저장
            dash_target = new Vector3(player_transform.position.x, player_transform.position.y, 0);
            Invoke("Dash_set", 1f);
            dash_cooltime = 0;
        }
        //돌진 발동
        if(dash)
        {
            //인식했던 위치에 돌진
            transform.position = Vector2.MoveTowards(transform.position, dash_target + offset, dash_speed * Time.deltaTime);
            //시간이 지날수록 속도 감소
             dash_speed -= Time.deltaTime * 80f;
            //돌진 속도가 0이면 중지
            if(dash_speed <= 0)
            {
                dash = false;
                enemyMove.moveable = true;
                
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //돌진중 플레이어와 접촉시 공격
        if(other.gameObject.tag == "Player" && dash)
        {
            GameManager.player_current_HP -= dash_power;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        //플레이어와 접충중일때 공격
        if (other.gameObject.tag == "Player"  && distance < 4 && dash == false)
        {
            InvokeRepeating("attack",0f,1.3f);
        }
    }
    void attack()
    {
        animator.SetTrigger("Attack 3");
    }
    void Dash_set()
    {
        animator.SetTrigger("Attack 3");
        //돌진 속도 갱신
        dash_speed = 40;
        //돌진 발동
        dash = true;
    }
    
    
    
}
