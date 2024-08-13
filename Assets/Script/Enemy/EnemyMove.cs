using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    //적 속도, 인식 범위, 공격 범위
    public float enemy_speed = 3;
    public float enemy_recognition_range = 10;
    public float enemy_attack_range = 1;

    void Start()
    {
        // 플레이어 타겟
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(enemy_attack_range < Vector2.Distance(transform.position, target.position) &&  enemy_recognition_range> Vector2.Distance(transform.position, target.position))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemy_speed*Time.deltaTime);
        }
    }
}
