using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Newgame : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("DontDestroyObject");
    }
}
