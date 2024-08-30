using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    bool has_attacked = false;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            //무기가 적에게 닿으면 공격력만큼 적의 체력 감소
            if(player.on_attack && !has_attacked)//플레이어가 공격중일때+데미지를 주지 않았을 때
            {
        
                //검은 데미지 2배
                //활과 데미지가 같으면 원거리공격인 활만씀
                EnemyData enemy_data = other.GetComponent<EnemyData>();
                
                if(enemy_data == null)
                {
                    enemy_data = other.GetComponentInParent<EnemyData>();
                }
                if(player.weapon_mode == 0 || player.weapon_mode == 2)
                {
                    enemy_data.enemy_current_HP -= GameManager.player_power * 2;
                    Debug.Log("검 공격");
                }
                else if(player.weapon_mode == 1)
                {
                    enemy_data.enemy_current_HP -= GameManager.player_power / 2;
                }
                has_attacked = true;
                Invoke("ToHasntAttacked", 0.4f);
                enemy_data.Hitted();
            }
        }
        
    }

    void ToHasntAttacked()
    {
        has_attacked = false;
    }
}
