using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordMan : MonoBehaviour
{
    Animator animator;
    EnemyData enemy_data;
    EnemyMove enemy_move;

    public int attack_mode = -1;
    public bool attack_able = true;//공격함수 중첩 방지
    public int random_charge;//두가지 차지중 하나 발동
    public bool on_red_charge = false;//반격기 터뜨리는 용도
    public bool on_blue_charge = false;
    public GameObject red_effect;
    public GameObject blue_effect;
    public int blue_charge_HP;
    public int blue_charge_HP_Neutralize = 60;

    public GameObject boom;
    public Transform boom_position;
    GameObject player;
    Player player_script;

    public List<int> cooltimes = new List<int>();
    public bool able0 = true;
    public bool able1 = true;
    public bool able2 = true;
    public bool able3 = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemy_data = GetComponent<EnemyData>();
        enemy_move = GetComponent<EnemyMove>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<Player>();
    }

    void Update()
    {
        if(able0 && attack_able)
        {
            enemy_move.moveable = false;
            animator.SetTrigger("Attack");
            able0 = false;
            attack_able = false;
            attack_mode = 0;
            Invoke("ToAttackAble", cooltimes[0]);
        }

        if(able1 && attack_able)
        {
            enemy_move.moveable = false;
            animator.SetTrigger("AttackDown");
            able1 = false;
            attack_able = false;
            Invoke("ToAttackDownAble", cooltimes[1]);
        }

        if(able2 && attack_able)
        {
            enemy_move.moveable = false;
            animator.SetTrigger("StingAndParrying");
            able2 = false;
            attack_able = false;
            attack_mode = 2;
            Invoke("ToStingAndParryingAble", cooltimes[2]);
        }

        if(able3 && attack_able)
        {
            enemy_move.moveable = false;
            animator.SetTrigger("Charge");
            able3 = false;
            attack_able = false;
            random_charge = UnityEngine.Random.Range(0, 2);
            if(random_charge == 0)
            {
                Invoke("RedCharge", 0.5f);
            }
            else if(random_charge == 1)
            {
                Invoke("BlueCharge", 0.5f);
            }
            Invoke("ToChargeAble", cooltimes[3]);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerWeapon" && on_red_charge && player_script.on_attack)
        {
            transform.Translate((player.transform.position-transform.position).normalized *100 * Time.deltaTime);
            red_effect.SetActive(false);
            animator.SetTrigger("Dash");
            attack_mode = 3;
        }
    }

    void RedCharge()
    {
        on_red_charge = true;
        red_effect.SetActive(true);
        Invoke("EffectOff", 2f);
    }

    public void Parrying()
    {
        animator.SetTrigger("Neutralize");
        attack_able = false;
    }

    void BlueCharge()
    {
        on_blue_charge = true;
        blue_effect.SetActive(true);
        blue_charge_HP = enemy_data.enemy_current_HP;
    }

    void EffectOff()
    {
        red_effect.SetActive(false);
        blue_effect.SetActive(false);
        on_red_charge = false;
    }

    void ToIdle()
    {
        Debug.Log("toidle");
        enemy_move.moveable = true;
        attack_mode = -1;
        attack_able = true;
        EffectOff();
        if(on_blue_charge && (enemy_data.enemy_current_HP - blue_charge_HP) > blue_charge_HP_Neutralize)
        {
            animator.SetTrigger("Neutralize");
            attack_able = false;
            on_blue_charge = false;
        }
    }

    void ToAttackAble()
    {
        able0 = true;
    }

    void ToAttackDownAble()
    {
        able1 = true;
    }

    void ToStingAndParryingAble()
    {
        able2 = true;
    }

    void ToChargeAble()
    {
        able3 = true;
    }

    void ToParrying()
    {
        attack_mode = -2;
    }

    void AttackDown()
    {
        attack_mode = 1;
    }
}
