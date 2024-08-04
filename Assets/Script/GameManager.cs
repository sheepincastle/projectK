using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //gamemanager가 모든 씬에서 같은 오브젝트로 존재하게 함
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    private static GameManager instance = null;

    //player 기본 정보 선언, static으로 지정하여 어디서든 같은 값을 가져올 수 있게 함
    public static int current_stage=0;
    public static int player_level=0;
    public static int player_speed=5;
    public static int player_power=10;
    public static int player_HP=100;
    public static int player_current_HP=100;
    
    //gamemanager 초기화
    void Awake()
    {
        //씬에 gamemanager가 있는지 확인 후 있으면 이 오브젝트 삭제
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        //씬이 바뀌어도 오브젝트 유지
        DontDestroyOnLoad(gameObject);
    }
}
