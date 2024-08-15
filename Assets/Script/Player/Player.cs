using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] Animator bow_animator;
    
    Animator animator;

    //모션 캔슬 후 공격 방지
    public bool on_attack = false;
    //0: 검, 1: 활
    public int weapon_mode;
    public GameObject sword_effect;

    void Awake()
    {
        animator = GetComponent<Animator>();
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
        if(Input.GetMouseButtonDown(0) && !on_attack)
        {
            on_attack = true;
            switch(weapon_mode)
            {
                case 0://검일 때
                {
                    sword_effect.SetActive(true);
                    animator.SetInteger("Attack", 0);
                    if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    Invoke("ToIdle", 0.68f);
                    break;
                }
                case 1://활일 때
                {
                    animator.SetInteger("Attack", 1);
                    bow_animator.enabled = true;
                    if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    Invoke("ToIdle", 0.833f);
                    break;
                }
            }
        }
        //테스트용
        if(Input.GetKeyDown(KeyCode.J))
            Attacked(0);
        else if(Input.GetKeyDown(KeyCode.K))
            Attacked(1);
    }

    void ToIdle()
    {
        animator.SetInteger("Attack", -1);
        on_attack = false;
        sword_effect.SetActive(false);
    }

    //적 공격에서 상태이상 부여 및 종류 결정
    //GameManager에서 player_current_HP값을 직접 수정해도 됨
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
