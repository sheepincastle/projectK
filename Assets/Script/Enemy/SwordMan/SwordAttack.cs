using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public SwordMan sword_man;
    public EnemyData enemy_data;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(sword_man.attack_mode)
            {
                case 0:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power;
                    player.GetComponent<Player>().Hited();
                    break;
                }
                case 1:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power;
                    player.GetComponent<Player>().Hited();
                    player.GetComponent<Player>().Attacked(1);
                    break;
                }
                case 2:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power/2;
                    player.GetComponent<Player>().Hited();
                    player.GetComponent<Player>().Attacked(0);
                    break;
                }
                case -2:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power*2;
                    player.GetComponent<Player>().Hited();
                    player.GetComponent<Player>().Attacked(1);
                    break;
                }
                case 3:
                {
                    GameManager.player_current_HP -= enemy_data.enemy_power*4;
                    player.GetComponent<Player>().Hited();
                    break;
                }
            }
        }

        if(other.tag == "PlayerWeapon" && sword_man.attack_mode == -2)
        {
            sword_man.Parrying();
        }
    }
}
