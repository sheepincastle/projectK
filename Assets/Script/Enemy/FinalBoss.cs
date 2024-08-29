using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    EnemyMove enemyMove;
    Animator animator;
    EnemyData enemyData;
    GameObject player;
    Transform player_transform;
    Player player_script;
    PlayerMove playerMove;
    Transform transform;
    
    public bool teleport = false;
    public float teleport_cooltime = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        enemyData = GetComponent<EnemyData>();
        player = GameObject.FindGameObjectWithTag("Player");
        player_transform = player.GetComponent<Transform>();
        player_script = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        
    }

    // Update is called once per frame
    void Update()
    {
        teleport_cooltime += Time.deltaTime;
        if(teleport_cooltime >= 10 && enemyData.enemy_current_HP > 0)
        {
            if(playerMove.player_direction == 0)
            {
                gameObject.transform.position = new Vector2(player_transform.position.x - 1, player_transform.position.y+1);
                teleport = true;
                enemyMove.moveable = false;
                teleport_cooltime = 0;
                Invoke("Moveable", 1);
            }
            else if(playerMove.player_direction == 1)
            {
                gameObject.transform.position = new Vector2(player_transform.position.x + 1, player_transform.position.y +1);
                teleport = true;
                enemyMove.moveable = false;
                teleport_cooltime = 0;
                Invoke("Moveable", 1);
            }



        }
    }

    void Moveable()
    {
        enemyMove.moveable = true;
    }
}


