using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.current_stage == 1)
            gameObject.transform.position = new Vector2(0, 0);
        else if(GameManager.current_stage == 2)
            gameObject.transform.position = new Vector2(0, 0);
        else if (GameManager.current_stage == 3)
            gameObject.transform.position = new Vector2(0, 0);
        else if (GameManager.current_stage == 4)
            gameObject.transform.position = new Vector2(0, 0);
        else if (GameManager.current_stage == 10)
            gameObject.transform.position = new Vector2(0, 0);
        else if (GameManager.current_stage == 20)
            gameObject.transform.position = new Vector2(0, 0);
        else
            gameObject.transform.position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
