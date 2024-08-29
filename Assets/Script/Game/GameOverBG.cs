using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class GameOverBG : MonoBehaviour
{
    public GameObject gameoverUI;
    public GameObject button;
    public GameObject player;
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        gameoverUI.SetActive(false);
        button.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.player_current_HP <= 0)
        {
            gameoverUI.SetActive(true);
            button.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void Restart()
    {
        Time.timeScale = 1;
        gameoverUI.SetActive(false);
        button.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.player_current_HP = 100;
        player.transform.position = spawn.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        spawn = GameObject.FindGameObjectWithTag("Spawn");

    }
}
