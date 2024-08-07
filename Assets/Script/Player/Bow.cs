using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    public Sprite bow_idle;
    public GameObject arrow;
    Vector3 mouse_position;
    bool was_animator_active=false;
    
     void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        animator.enabled = false;
    }

    void Update()
    {
        //애니메이터가 켜지면 is_animator_active를 참으로함
        //->is_animator_active는 참, was_animator_active는 거짓인 상태에서 화살 발사
        //->was_animator_active를 참으로 바꿔 화살을 더 발사하지 않게 함
        //->애니메이터가 꺼진 후 was_animator_active를 거짓으로 하여 초기화
        if(animator!=null)
        {
            bool is_animator_active = animator.enabled;
            if(is_animator_active && !was_animator_active)
            {
                mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_position.z = 0;
                Invoke("Shoot", 0.417f);
            }
            was_animator_active = is_animator_active;
        }
        else
        {  
            was_animator_active = false;
        }
        
    }

    //활을 쐈을 때 기본 상태로 돌아감
    //화살 발사
    void Shoot()
    {
        animator.enabled = false;//활 애니메이션 종료
        sprite.sprite = bow_idle;//활을 평상시 형태로
        GameObject intant_arrow = Instantiate(arrow, transform.position, Quaternion.Euler(0,0,0));//화살소환
        Arrow arrow_script = intant_arrow.GetComponent<Arrow>();//화살의 Arrow 스크립트 가져옴

        arrow_script.Fire(mouse_position);//발사
    }

}
