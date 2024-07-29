using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }

    //피격 이벤트
    //적의 공격 스크립트에서 작동, 플레이어에게 줄 데미지를 함께 써야함
    public void Attacked(int damage)
    {
        GameManager.player_HP -= damage;
    }
}
