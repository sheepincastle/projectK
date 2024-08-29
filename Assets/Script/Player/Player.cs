using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] List<GameObject> weapons = new List<GameObject>();
    [SerializeField] Animator bow_animator;
    
    Animator animator;
    public GameObject hitted_image;
    Vector3 dash_direction;

    public bool on_attack = false;

    //대쉬기능을 위한 변수들
    public bool on_dash = false;
    public float dash_cooltime = 10;
    public bool dash_able = true;
    public float dash_duration = 0.5f;
    public float dash_time;
    public UnityEngine.UI.Image dash_cool_down;


    //0: 검, 1: 활
    public int weapon_mode;
    public GameObject sword_effect;
    public bool weakeningable = true;
    public bool midboss_cleared = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hitted_image.SetActive(false);
        //검만 활성화
        weapon_mode = 0;
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        animator.SetInteger("Attack", -1);
        animator.SetInteger("Attacked", -1);
    }

    void Update()
    {
        //tap키로 무기 변환
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!midboss_cleared)
            {
                switch (weapon_mode)
                {
                    case 0://검일 때
                        {
                            weapon_mode = 1;
                            weapons[0].SetActive(false);
                            weapons[1].SetActive(true);
                            break;
                        }
                    case 1://활일 때
                        {
                            weapon_mode = 0;
                            weapons[0].SetActive(true);
                            weapons[1].SetActive(false);
                            break;
                        }
                }
            }
            else
            {
                switch (weapon_mode)
                {
                    case 2://검일 때
                        {
                            weapon_mode = 1;
                            weapons[2].SetActive(false);
                            weapons[1].SetActive(true);
                            break;
                        }
                    case 1://활일 때
                        {
                            weapon_mode = 2;
                            weapons[2].SetActive(true);
                            weapons[1].SetActive(false);
                            break;
                        }
                }
            }
        }
        
        //우클릭 시 일반 공격
        if(Input.GetMouseButtonDown(0) && !on_attack && !on_dash)
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
                case 2:
                    {
                        sword_effect.SetActive(true);
                        animator.SetInteger("Attack", 0);
                        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
                        {
                            transform.localScale = new Vector3(-1, 1, 1);
                        }
                        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
                        {
                            transform.localScale = new Vector3(1, 1, 1);
                        }
                        Invoke("ToIdle", 0.68f);
                        break;
                    }
            }
        }
        //테스트용
        if (Input.GetKeyDown(KeyCode.J))
            WeaponUpgrade();

        if(Input.GetKeyDown(KeyCode.Space)&& dash_able)
        {
            dash_time = 0;
            dash_able = false;
            on_dash = true;
            //대쉬하는동안 다른 움직임x
            on_attack = false;
            PlayerMove.moveable = false;
            bow_animator.enabled = false;
            animator.SetTrigger("Dash");
            sword_effect.SetActive(false);
            dash_cool_down.fillAmount = 1;

            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = transform.position.z;
            dash_direction = (mouse- transform.position).normalized;
            if(mouse.x > transform.position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if(mouse.x < transform.position.x)
                transform.localScale = new Vector3(1, 1, 1);

            StartCoroutine(Dash());
        }
    }

    void ToIdle()
    {
        animator.SetInteger("Attack", -1);
        on_attack = false;
        sword_effect.SetActive(false);
    }

    //적 공격에서 상태이상 부여 및 종류 결정
    //GameManager에서 player_current_HP값을 직접 수정해도 됨
    //0: 경직, 1: 
    public void Attacked(int Attacked)
    {
        if(!on_dash)
        {
            animator.SetInteger("Attacked", Attacked);
            PlayerMove.moveable = false;
            if (Attacked == 0)
                Invoke("ReMove", 0.2f);
            else if (Attacked == 1)
                Invoke("ReMove", 1);
        }  
    }

    void ReMove()
    {
        animator.SetInteger("Attacked", -1);
        PlayerMove.moveable = true;
    }

    IEnumerator Dash()
    {
        while(dash_time < dash_cooltime)
        {
            dash_time += Time.deltaTime;

            if(dash_time > 0.25f && dash_time < 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + dash_direction, 20 * Time.deltaTime);
            }
            else if(dash_time >= 0.5f)
            {
                on_dash = false;
                ReMove();
            }
            
            dash_cool_down.fillAmount = 1- (dash_time/dash_cooltime);//쿨타임 이미지 작동

            yield return null;
        }

        dash_cool_down.fillAmount = 0;
        dash_able = true;
    }

    public void Hited()
    {
        hitted_image.SetActive(true);
        Invoke("ToOriginColor", 0.3f);
        Debug.Log(GameManager.player_current_HP);
    }

    void ToOriginColor()
    {
        hitted_image.SetActive(false);
    }

    public void WeaponUpgrade()
    {
        midboss_cleared = true;
        GameManager.player_power += 7;
        weapons[0].SetActive(false);
        switch (weapon_mode)
        {
            case 0://검일 때
                {
                    weapon_mode = 2;
                    weapons[2].SetActive(true);
                    weapons[1].SetActive(false);
                    break;
                }
            case 1://활일 때
                {
                    weapon_mode = 1;
                    weapons[2].SetActive(false);
                    weapons[1].SetActive(true);
                    break;
                }
        }
    }
}
