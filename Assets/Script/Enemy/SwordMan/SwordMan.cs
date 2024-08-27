using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : MonoBehaviour
{
    Animator animator;

    public int attack_mode = -1;
    public List<GameObject> attack_areas = new List<GameObject>();
    List<DetecPlayer> detect_player = new List<DetecPlayer>();
    GameObject player;

    public List<int> cooltimes = new List<int>();
    List<bool> are_able = new List<bool>();

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        detect_player = new List<DetecPlayer>(attack_areas.Count);
        are_able = new List<bool>(attack_areas.Count);

        for(int i = 0; i < attack_areas.Count; i++)
        {
            detect_player.Add(attack_areas[i].GetComponent<DetecPlayer>());
            are_able.Add(true);
        }
    }

    void Update()
    {
        if(detect_player[0].in_area && are_able[0])
        {
            animator.SetTrigger("Attack");
            are_able[0] = false;
            attack_mode = 0;
            Invoke("ToIdle", 0.833f);
            Invoke("ToAttackAble", cooltimes[0]);
        }
    }

    void ToIdle()
    {
        attack_mode = -1;
    }

    void ToAttackAble()
    {
        are_able[0] = true;
    }
}
