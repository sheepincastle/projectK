using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0, 1, 0);
    //?? ???, ?��? ????, ???? ????
    public float enemy_speed = 3;
    public float enemy_recognition_range = 10;
    public float enemy_attack_range = 1;

    //public float enemy_attack_range=1;
    //���� ���� �÷��̾ �����Ҽ� ���� ���� �߻�->������� �����ϵ��� ����
    public bool enemy_attack_range_enabled = false;
    //��ų ����� �������� �ʰ� �ϱ� ���� ����
    public bool moveable = true;
    //���� ���� ũ�⿡ ������� �¿��Ī�� �ϱ� ���� ��
    float origin_x;
    Animator animator;

    void Start()
    {
        // ?��???? ???
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        origin_x = transform.localScale.x;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position + offset);
        if(moveable &&  enemy_recognition_range > distance)
        {
            //�����϶�
            animator.SetBool("Run", true);
            //�� �ִϸ����͸� ���� �� bool�� 'Run'�� �̿��� �޸��� ��� �����ϱ�
            transform.position = Vector2.MoveTowards(transform.position, target.position + offset, enemy_speed*Time.deltaTime);

            //���� ����� �ݴ�� ��Ÿ���� ��ũ��Ʈ�� �ƴ϶� ����Ƽ���� scale�� x�� ��ȣ�� �ݴ�� �س���
            if(target.position.x > transform.position.x)//�÷��̾ ���� �����ʿ� ���� ��
            {
                transform.localScale = new Vector3(origin_x, transform.localScale.y, transform.localScale.z);//�״��
            }
            else if(target.position.x < transform.position.x)//�÷��̾ ���� ���ʿ� ���� ��
            {
                transform.localScale = new Vector3(origin_x * -1, transform.localScale.y, transform.localScale.z);//�¿����
            }
        }
        else
        {
            //�ȿ����϶�
            animator.SetBool("Run", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")//�÷��̾�� �����ϸ� �������� ����
        {
            moveable = false;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")//�÷��̾�� �������� �����ϼ� ����
        {
            moveable = true;
        }
    }
}
