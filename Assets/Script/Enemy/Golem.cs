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

    float boom_abletime = 27;
    float bat_abletime = 18;
    float dash_abletime = 10;
    bool boom = false;
    float boom_range = 3;
    float boom_cooltime = 23;
    float batspwan_cooltime = 0;
    public GameObject boomPrepab;
    public GameObject batPrepab;
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
        //박쥐소환 쿨타임 갱신
        batspwan_cooltime += Time.deltaTime;
        //폭발 쿨타임 갱신
        boom_cooltime += Time.deltaTime;
        //박쥐소환쿨타임이 설정값 이상일때 박쥐소환
        if(batspwan_cooltime >= bat_abletime && dash == false && enemyData.enemy_current_HP > 0 && boom == false)
        {
            enemyMove.moveable = false;
            animator.SetTrigger("Ability");
            batspwan_cooltime = 0;
            GameObject bat1 = Instantiate(batPrepab, new Vector2(transform.position.x -3, transform.position.y), Quaternion.identity);
            GameObject bat2 = Instantiate(batPrepab, new Vector2(transform.position.x, transform.position.y + 3), Quaternion.identity);
            GameObject bat3 = Instantiate(batPrepab, new Vector2(transform.position.x + 3, transform.position.y), Quaternion.identity);
            Invoke("movestart", 0.5f);
        }
        //폭발 쿨타임이 설정값 이상일때 폭발
        if(boom_cooltime >= boom_abletime && batspwan_cooltime < bat_abletime && batspwan_cooltime > 1 && dash == false && enemyData.enemy_current_HP > 0)
        {
            boom = true;
            enemyMove.moveable = false;
            animator.SetTrigger("Attack");
            boom_cooltime = 0;
            Invoke("boom1", 1.2f);  
        }
        //죽음
        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 1);
        }
        //일정거리에 플레이어가 있고 돌진쿨타임이 설정값 이상일때 돌진 준비
        if(dash_cooltime >= dash_abletime && distance >= 6 && distance <= 12 && enemyData.enemy_current_HP > 0 && boom == false && boom_cooltime < boom_abletime && batspwan_cooltime < bat_abletime && boom_cooltime > 5)
        {
            //잠시 정지
            enemyMove.moveable = false;
            //타겟 위치 저장
            dash_target = new Vector3(player_transform.position.x, player_transform.position.y, 0);
            dash_cooltime = 0;
            Invoke("Dash_set", 1f);
            
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
            other.gameObject.GetComponent<Player>().Hited();
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
        player.gameObject.GetComponent<Player>().Hited();
        GameManager.player_current_HP -= enemyData.enemy_power;
    }
    void Dash_set()
    {
        animator.SetTrigger("Attack 3");
        //돌진 속도 갱신
        dash_speed = 40;
        //돌진 발동
        dash = true;
    }
    void movestart()
    {
        enemyMove.moveable = true;
    }
    //1차 폭발
    void boom1()
    {
        animator.SetTrigger("Ability");
        for(int i = 1; i <= boom_range; i++)
        {
            GameObject boom1 = Instantiate(boomPrepab, new Vector2(transform.position.x - 5*i, transform.position.y), Quaternion.identity);
            GameObject boom2 = Instantiate(boomPrepab, new Vector2(transform.position.x + 5*i, transform.position.y), Quaternion.identity);
            GameObject boom3 = Instantiate(boomPrepab, new Vector2(transform.position.x , transform.position.y - 5*i), Quaternion.identity);
            GameObject boom4 = Instantiate(boomPrepab, new Vector2(transform.position.x , transform.position.y + 5*i), Quaternion.identity);
        }
        Invoke("boom2", 1.2f);
    }
    //2차 폭발
    void boom2()
    {
        animator.SetTrigger("Ability");
        for (int i = 1; i <= boom_range; i++)
        {
            GameObject boom1 = Instantiate(boomPrepab, new Vector2(transform.position.x - 3 * i, transform.position.y + 3*i), Quaternion.identity);
            GameObject boom2 = Instantiate(boomPrepab, new Vector2(transform.position.x - 3 * i, transform.position.y -3*i), Quaternion.identity);
            GameObject boom3 = Instantiate(boomPrepab, new Vector2(transform.position.x + 3*i, transform.position.y - 3 * i), Quaternion.identity);
            GameObject boom4 = Instantiate(boomPrepab, new Vector2(transform.position.x + 3 *i, transform.position.y + 3 * i), Quaternion.identity);
        }
        Invoke("boom3", 1.2f);
    }
    //3차 폭발
    void boom3()
    {
        animator.SetTrigger("Ability");
        GameObject boom1 = Instantiate(boomPrepab, new Vector2(transform.position.x - 5 , transform.position.y), Quaternion.identity);
        GameObject boom2 = Instantiate(boomPrepab, new Vector2(transform.position.x + 5 , transform.position.y), Quaternion.identity);
        GameObject boom3 = Instantiate(boomPrepab, new Vector2(transform.position.x, transform.position.y - 5 ), Quaternion.identity);
        GameObject boom4 = Instantiate(boomPrepab, new Vector2(transform.position.x, transform.position.y + 5 ), Quaternion.identity);
        GameObject boom5 = Instantiate(boomPrepab, new Vector2(transform.position.x - 3 , transform.position.y + 3), Quaternion.identity);
        GameObject boom6 = Instantiate(boomPrepab, new Vector2(transform.position.x - 3 , transform.position.y - 3 ), Quaternion.identity);
        GameObject boom7 = Instantiate(boomPrepab, new Vector2(transform.position.x + 3 , transform.position.y - 3 ), Quaternion.identity);
        GameObject boom8 = Instantiate(boomPrepab, new Vector2(transform.position.x + 3 , transform.position.y + 3 ), Quaternion.identity);
        Invoke("movestart", 1f);
        boom = false;
    }
    
}
