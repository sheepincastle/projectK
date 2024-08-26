using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //적에 대한 정보
    //필요시 public으로 추가
    //수치 설정은 인스턴스창에서
    //적의 collider와 같은곳에 위치해야됨
    public int enemy_HP;
    public int enemy_current_HP;
    public int enemy_power;
    
    SpriteRenderer sprite;

    void Start()
    {
        enemy_current_HP = enemy_HP;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(enemy_current_HP >enemy_HP)//적의 체력이 최대체력보다 많아지면 조절
        {
            enemy_current_HP = enemy_HP;
        }
    }

    public void Hitted()
    {
        sprite.color = new Color(255/255, 167/255, 167/255, 255/255);
        Invoke("ToOriginColor", 0.5f);
    }

    void ToOriginColor()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }
}
