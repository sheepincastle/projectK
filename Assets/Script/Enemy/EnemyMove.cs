using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    //�� �ӵ�, �ν� ����, ���� ����
    public float enemy_speed = 3;
    public float enemy_recognition_range = 10;
    public float enemy_attack_range = 1;

    void Start()
    {
        // �÷��̾� Ÿ��
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
