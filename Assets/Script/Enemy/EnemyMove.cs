using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0, 1, 0);
    //?? ???, ?ν? ????, ???? ????
    public float enemy_speed = 3;
    public float enemy_recognition_range = 10;
    public float enemy_attack_range = 1;

    //public float enemy_attack_range=1;
    //작은 적은 플레이어를 공격할수 없는 버그 발생->닿았을때 정지하도록 수정
    public bool enemy_attack_range_enabled = false;
    //스킬 사용중 움직이지 않게 하기 위한 변수
    public bool moveable = true;
    //적의 원래 크기에 상관없이 좌우대칭을 하기 위한 값
    float origin_x;
    Animator animator;

    void Start()
    {
        // ?÷???? ???
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        origin_x = transform.localScale.x;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position + offset);
        if(moveable &&  enemy_recognition_range > distance)
        {
            //움직일때
            animator.SetBool("Run", true);
            //적 애니메이터를 만들 때 bool값 'Run'을 이용해 달리는 모습 구현하기
            transform.position = Vector2.MoveTowards(transform.position, target.position + offset, enemy_speed*Time.deltaTime);

            //적의 모습이 반대로 나타나면 스크립트가 아니라 유니티에서 scale의 x값 부호를 반대로 해놓기
            if(target.position.x > transform.position.x)//플레이어가 적의 오른쪽에 있을 때
            {
                transform.localScale = new Vector3(origin_x, transform.localScale.y, transform.localScale.z);//그대로
            }
            else if(target.position.x < transform.position.x)//플레이어가 적의 왼쪽에 있을 때
            {
                transform.localScale = new Vector3(origin_x * -1, transform.localScale.y, transform.localScale.z);//좌우반전
            }
        }
        else
        {
            //안움직일때
            animator.SetBool("Run", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")//플레이어와 접촉하면 움직이지 않음
        {
            moveable = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")//플레이어와 떨어지면 움직일수 있음
        {
            moveable = true;
        }
    }
}
