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

    void Start()
    {
        enemy_current_HP = enemy_HP;
    }
}
