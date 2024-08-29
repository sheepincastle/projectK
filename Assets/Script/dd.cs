using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        Debug.Log("Newgame");
    }
    public void Load()
    {
        switch(PlayerPrefs.GetInt("Stage"))
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 10:
                break;
            case 20:
                break;
        }
    }
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
}
}
