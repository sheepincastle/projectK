using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordMan : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid;
    EnemyData enemy_data;
    EnemyMove enemy_move;

    public int attack_mode = -1;
    public bool attack_able = true;//공격함수 중첩 방지

    public GameObject boom;
    public Transform boom_position;

    public GameObject red_effect;
    public GameObject blue_effect;
    public bool on_red = false;
    int blue_HP;

    GameObject player;
    Player player_script;

    public List<float> cooltimes = new List<float>();
    public bool able0 = true;
    public bool able1 = true;
    public bool able2 = true;
    public bool able3 = true;

    bool dead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        enemy_data = GetComponent<EnemyData>();
        enemy_move = GetComponent<EnemyMove>();
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<Player>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        Ray ray = new Ray(transform.position, Vector2.down);//보스 바로 앞에 아래를 향한 ray 생성
        Ray ray2 = new Ray(transform.position + new Vector3(0.1f, 0, 0), Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.4f);
        RaycastHit2D hit2 = Physics2D.Raycast(ray2.origin, ray2.direction, 0.4f);
        if(distance < 4)
        {
            if(able0 && attack_able)
                StartCoroutine(Attack());

            if(able2 && attack_able)
                StartCoroutine(StingAndParrying());

            if(able3 && attack_able)
                StartCoroutine(Charge());
        }
        else if(hit.collider != null && hit.collider.transform == player.transform)//플레이어가 아래에 있을 때 공격
        {
            if(able1 && attack_able)
                StartCoroutine(AttackDown());
        }
        else if(hit2.collider != null && hit2.collider.transform == player.transform)//플레이어가 아래에 있을 때 공격
        {
            if(able1 && attack_able)
                StartCoroutine(AttackDown());
        }

        if(enemy_data.enemy_current_HP <= 0 && !dead)
        {
            Debug.Log("디짐");
            dead = true;
            animator.SetTrigger("Neutralize");
            rigid.velocity = Vector3.zero;
            attack_able = false;
            enemy_move.moveable = false;
            Destroy(gameObject, 2f);
        }
    }

    IEnumerator Attack()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        able0 = false;
        attack_able = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        attack_mode = 0;
        yield return new WaitForSeconds(0.33333333333f);
        attack_mode = -1;
        yield return new WaitForSeconds(0.5f);
        attack_able = true;
        enemy_move.moveable = true;
        yield return new WaitForSeconds(cooltimes[0] - 1.3333333333f);
        able0 = true;
    }

    IEnumerator AttackDown()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        able1 = false;
        attack_able = false;
        animator.SetTrigger("AttackDown");
        yield return new WaitForSeconds(1.1666666666f);
        attack_mode = 1;
        yield return new WaitForSeconds(0.3333333333f);
        attack_mode = -1;
        GameObject boom1 = Instantiate(boom, boom_position.position, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(0.5f);
        attack_able = true;
        enemy_move.moveable = true;
        yield return new WaitForSeconds(cooltimes[1] - 2f);
        able1 = true;
    }

    IEnumerator StingAndParrying()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        able2 = false;
        attack_able = false;
        animator.SetTrigger("StingAndParrying");
        yield return new WaitForSeconds(0.5f);
        attack_mode = 2;
        yield return new WaitForSeconds(2.5f);
        attack_mode = -1;
        yield return new WaitForSeconds(0.3333333333333f);
        attack_mode = -2;
        yield return new WaitForSeconds(0.3333333333333f);
        attack_mode = -1;
        yield return new WaitForSeconds(0.5f);
        attack_able = true;
        enemy_move.moveable = true;
        yield return new WaitForSeconds(cooltimes[2] - 4.16666666666f);
        able2 = true;
    }

    public void Parrying()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        attack_able = false;
        animator.SetTrigger("Neutralize");
        Invoke("ToIdle", 5f);
    }

    IEnumerator Charge()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        able3 = false;
        attack_able = false;
        animator.SetTrigger("Charge");
        int randint = UnityEngine.Random.Range(0, 2);
        yield return new WaitForSeconds(0.5f);
        if(randint == 0)
        {
            red_effect.SetActive(true);
            on_red = true;
            yield return new WaitForSeconds(2f);
            red_effect.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            if(on_red)//반격 터지지 않았을때
            {
                enemy_move.moveable = true;
                attack_able = true;
                on_red = false;
            }
        }
        else if(randint == 1)
        {
            blue_effect.SetActive(true);
            blue_HP = enemy_data.enemy_current_HP;
            yield return new WaitForSeconds(2f);
            blue_effect.SetActive(false);
            if(enemy_data.enemy_current_HP < blue_HP - 60)
                Parrying();
            else
            {
                enemy_move.moveable = true;
                attack_able = true;
            }
        }
        yield return new WaitForSeconds(cooltimes[3] - 3f);
        able3 = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(on_red && other.tag == "PlayerWeapon" && player_script.on_attack)
            StartCoroutine(Dash());
    }

    IEnumerator Dash()//반격 터짐
    {
        Debug.Log("반격");
        on_red = false;
        red_effect.SetActive(false);
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = false;
        attack_able = false;
        animator.SetTrigger("Dash");
        yield return new WaitForSeconds(0.2f);

        Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

        //충돌 무시
        foreach(Collider2D collider in childColliders)
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collider, true);
        foreach(Collider2D collider in childColliders)
            Physics2D.IgnoreCollision(GameObject.Find("wall").GetComponent<Collider2D>(), collider, true);

        Vector2 start_position = transform.position;
        Vector2 target_position = player.transform.position;
        yield return StartCoroutine(MoveToPosition(target_position, 0.1333333333333333f));//플레이어 향해 돌진


        Vector2 overshoot_position = target_position + (target_position - start_position).normalized * 20;//더 감
        yield return StartCoroutine(MoveToPosition(overshoot_position, 0.5f));

        yield return new WaitForSeconds(0.166666666666666f);
        yield return StartCoroutine(MoveToPosition(start_position, 0.5f));//귀환
        rigid.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.2f);
        GameManager.player_current_HP -= enemy_data.enemy_power*4;//공격
        player_script.Hited();
        yield return new WaitForSeconds(0.8f);

        //충돌 무시 해제
        foreach(Collider2D collider in childColliders)
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), collider, false);
        foreach(Collider2D collider in childColliders)
            Physics2D.IgnoreCollision(GameObject.Find("wall").GetComponent<Collider2D>(), collider, false);
        attack_able = true;
        enemy_move.moveable = true;
    }

    IEnumerator MoveToPosition(Vector2 destination, float timeToMove)
    {
        Vector2 currentPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector2.Lerp(currentPos, destination, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }

    void ToIdle()
    {
        rigid.velocity = Vector3.zero;
        enemy_move.moveable = true;
        attack_able = true;
    }
}
