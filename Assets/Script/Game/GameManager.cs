using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //player 기본 정보 선언, static으로 지정하여 어디서든 같은 값을 가져올 수 있게 함
    public static int current_stage=-1;

    public static int player_level=0;
    public static int player_speed=8;
    public static int player_power=10;
    public static int player_HP=100;
    public static int player_current_HP=100;

    public GameObject stored;

    void Start()
    {
        Time.timeScale = 1;
        player_current_HP = player_HP;
        
        switch(SceneManager.GetActiveScene().name)
        {
            case "Stage1":
                current_stage=1;
                break;
            case "Stage2":
                current_stage=2;
                break;
            case "Stage3":
                current_stage=3;
                break;
            case "Stage4":
                current_stage=4;
                break;
            case "MidBoss":
                current_stage=10;
                break;
            case "FinalBoss":
                current_stage=20;
                break;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            if(!PlayerPrefs.HasKey("Stage"))
            {
                PlayerPrefs.SetInt("Stage", current_stage);
                stored.SetActive(true);
                Invoke("StoredDisable", 1);
            }
        }
    }

    void StoredDisable()
    {
        stored.SetActive(false);
    }
}
