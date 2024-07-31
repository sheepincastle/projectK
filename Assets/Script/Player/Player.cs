using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Transform player_transform;
    Rigidbody2D rigid;
    //0: 검, 1: 활
    [SerializeField] int weapon_mode;
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] Animator bow_animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player_transform = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        //검만 활성화
        weapon_mode = 0;
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        animator.SetInteger("Attack", -1);
        animator.SetInteger("Attacked", -1);
    }

    void Update()
    {
        //tap키로 무기 변환
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            switch(weapon_mode)
            {
                case 0://검일 때
                {
                    weapon_mode=1;
                    weapons[0].SetActive(false);
                    weapons[1].SetActive(true);
                    break;
                }
                case 1://활일 때
                {
                    weapon_mode=0;
                    weapons[0].SetActive(true);
                    weapons[1].SetActive(false);
                    break;
                }
            }
        }
        
        //우클릭 시 일반 공격
        if(Input.GetMouseButtonDown(0))
        {
            switch(weapon_mode)
            {
                case 0://검일 때
                {
                    animator.SetInteger("Attack", 0);
                    Invoke("ToIdle", 0.3f);
                    break;
                }
                case 1://활일 때
                {
                    animator.SetInteger("Attack", 1);
                    bow_animator.enabled = true;
                    Invoke("ToIdle", 0.3f);
                    break;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.J))
            Attacked(0);
        else if(Input.GetKeyDown(KeyCode.K))
            Attacked(1);
    }

    void ToIdle()
    {
        animator.SetInteger("Attack", -1);
    }

    //적 공격에서 상태이상 부여 및 종류 결정
    //0: 경직, 1: 스턴
    public void Attacked(int Attacked)
    {
        animator.SetInteger("Attacked", Attacked);
        PlayerMove.moveable = false;

        if(Attacked == 0)
            Invoke("ReMove", 0.2f);
        else if(Attacked == 1)
            Invoke("ReMove", 1);
    }

    void ReMove()
    {
        animator.SetInteger("Attacked", -1);
        PlayerMove.moveable = true;
    }
}
