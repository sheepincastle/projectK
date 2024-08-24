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
        //���� ��Ÿ�� ����
        dash_cooltime += Time.deltaTime;
        //����
        if (enemyData.enemy_current_HP <= 0)
        {
            animator.SetTrigger("Death");
            enemyMove.moveable = false;
            Destroy(gameObject, 1);
        }
        //�����Ÿ��� �÷��̾ �ְ� ������Ÿ���� ������ �̻��϶� ���� �غ�
        if(dash_cooltime >= 10 && distance >= 6 && distance <= 12)
        {
            //��� ����
            enemyMove.moveable = false;
            //Ÿ�� ��ġ ����
            dash_target = new Vector3(player_transform.position.x, player_transform.position.y, 0);
            Invoke("Dash_set", 1f);
            dash_cooltime = 0;
        }
        //���� �ߵ�
        if(dash)
        {
            //�ν��ߴ� ��ġ�� ����
            transform.position = Vector2.MoveTowards(transform.position, dash_target + offset, dash_speed * Time.deltaTime);
            //�ð��� �������� �ӵ� ����
             dash_speed -= Time.deltaTime * 80f;
            //���� �ӵ��� 0�̸� ����
            if(dash_speed <= 0)
            {
                dash = false;
                enemyMove.moveable = true;
                
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //������ �÷��̾�� ���˽� ����
        if(other.gameObject.tag == "Player" && dash)
        {
            GameManager.player_current_HP -= dash_power;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        //�÷��̾�� �������϶� ����
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
        //���� �ӵ� ����
        dash_speed = 40;
        //���� �ߵ�
        dash = true;
    }
    
    
    
}
