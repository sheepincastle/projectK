using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Transform player_transform;
    [SerializeField] bool moveable;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player_transform = GetComponent<Transform>();
        moveable = true;
    }

    void Update()
    {
        //움직일 수 있을때
        if(moveable)
        {
            //wasd로 이동, player_speed변수로 속도 조절
            float moveX = Input.GetAxisRaw("Horizontal")*GameManager.player_speed;
            float moveY = Input.GetAxisRaw("Vertical")*GameManager.player_speed;

            Vector3 move_vector= new Vector3(moveX, moveY, 0f);
            player_transform.Translate(move_vector*Time.deltaTime);
        

            //움직일 때 애니메이션 작동
            if(moveX!=0||moveY!=0)
            {
                animator.SetFloat("RunState", 1f);
            }
            else
            {
                animator.SetFloat("RunState", 0);
            }
            //방향전환
            if(moveX>0)
                player_transform.localScale = new Vector3(-1, 1, 1);
            else if(moveX<0)
                player_transform.localScale = new Vector3(1, 1, 1);
        
            //우클릭 시 일반 공격
            if(Input.GetMouseButtonDown(0))
                Attack(0f, 0f, 0f);
        }
        
        //피격 모션 테스트
        /*if(Input.GetKeyDown(KeyCode.F))
        {
            moveable=false;
            animator.SetTrigger("Attacked");
            animator.SetFloat("AttackedState", 0.75f);
        }
        */
    }

    void Attack(float attack_state, float normal_state, float skill_state)
    {
        moveable=false;
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackState", attack_state);
        animator.SetFloat("NormalState", normal_state);
        animator.SetFloat("SkillState", skill_state);
    }

    void OnMove()
    {
        moveable=true;
    }
}
