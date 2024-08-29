using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameOver : MonoBehaviour
{
    public GameObject restartUI;
    private bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        restartUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
