using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Load");
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
