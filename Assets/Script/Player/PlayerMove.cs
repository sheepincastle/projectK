using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Transform transform;
    void Awake()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        //wasd로 이동, player_speed변수로 속도 조절
        float moveX = Input.GetAxisRaw("Horizontal")*GameManager.player_speed;
        float moveY = Input.GetAxisRaw("Vertical")*GameManager.player_speed;

        Vector3 move_vector= new Vector3(moveX, moveY, 0f);
        transform.Translate(move_vector*Time.deltaTime);

        //움직일 때 애니메이션 작동
        if(moveX!=0||moveY!=0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

        //방향전환
        if(moveX>0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if(moveX<0)
            transform.localScale = new Vector3(1, 1, 1);
        

        //우클릭 시 일반 공격
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("NormalAttack");
        }
    }
}
