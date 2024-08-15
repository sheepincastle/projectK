using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //player 기본 정보 선언, static으로 지정하여 어디서든 같은 값을 가져올 수 있게 함
    public static int current_stage=0;
    public static int player_level=0;
    public static int player_speed=5;
    public static int player_power=10;
    public static int player_HP=100;
    public static int player_current_HP=100;
}
