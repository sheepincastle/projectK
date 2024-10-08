using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float moveX;
    float moveY;
    Rigidbody2D rigid;
    Animator animator;
    Transform player_transform;
    public static bool moveable;
    public int player_direction = 1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player_transform = GetComponent<Transform>();
        moveable = true;
    }

    void Update()
    {
        //wasd로 이동, player_speed변수로 속도 조절
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        

        //움직일 때 애니메이션 작동
        if((moveX!=0||moveY!=0) && animator.GetInteger("Attack")==-1 && animator.GetInteger("Attacked")==-1 && moveable)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        //방향전환
        if((moveX>0) && animator.GetInteger("Attack")==-1 && animator.GetInteger("Attacked")==-1 && moveable)
        {
            player_transform.localScale = new Vector3(-1, 1, 1);
            player_direction = 0;
        }
        else if((moveX<0) && animator.GetInteger("Attack")==-1 && animator.GetInteger("Attacked")==-1 && moveable)
        {
            player_transform.localScale = new Vector3(1, 1, 1);
            player_direction = 1;
        }
    }

    void FixedUpdate()
    {
        if(moveable)
            rigid.velocity = new Vector3(moveX, moveY, 0)*GameManager.player_speed;
        else
            rigid.velocity = Vector3.zero;
    }
}
