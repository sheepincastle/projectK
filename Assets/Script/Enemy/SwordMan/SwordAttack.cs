using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public SwordMan sword_man;
    public EnemyData enemy_data;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(sword_man.attack_mode)
            {
                case 0:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power;
                    break;
                }
            }
        }
    }
}
