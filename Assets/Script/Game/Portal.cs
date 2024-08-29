using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public String next_stage;
    public GameObject player;
    public GameObject real_player;
    public int nextstage;
    //로드할 씬의 좌표
    public float x;
    public float y;
    public float z;

    void Start()
    {
        player = GameObject.Find("Unit000");
        real_player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // 씬을 로드하고 콜백으로 OnSceneLoaded 메서드를 연결
            SceneManager.LoadScene(next_stage);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        player.transform.position = new Vector3(x, y, z);
        real_player.transform.localPosition = new Vector3(0, 0, 0);
        GameManager.current_stage = nextstage;

        
        //콜백 연결 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
