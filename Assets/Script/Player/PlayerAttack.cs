using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //무기가 적에게 닿으면 공격력만큼 적의 체력 감소
            if(player.on_attack)//플레이어가 공격중일때
            {
                //검은 여러번 공격함
                //활과 데미지가 같으면 원거리공격인 활만씀
                EnemyData enemy_data = other.GetComponent<EnemyData>();
                enemy_data.enemy_current_HP -= GameManager.player_power;
            }
        }
        
    }
}
