using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 direction;
    void OnEnable()
    {
        //3초 뒤 파괴
        Destroy(gameObject, 3f);
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Fire(Vector3 mouse)
    {
        direction = mouse - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;//마우스 방향으로 각도 계산
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+180));//화살의 각도 조정
        rigid.velocity = direction * 10 / Vector3.Distance(mouse, transform.position);//발사
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //투사체 또는 플레이어가 아닌 물체와 닿으면 파괴
        if(other.tag != "Bullet" || other.tag != "Player")
            Destroy(gameObject);
    }
}
