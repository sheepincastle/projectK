using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spwn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.current_stage == 1)
        {
            this.transform.position = new Vector2(-10, 0);
        }
        else if (GameManager.current_stage >= 2 && GameManager.current_stage <= 4)
        {
            Vector3 newPosition = Vector3.zero;

            if (GameManager.current_stage == 2)
                newPosition = new Vector2(-24, 7);
            else if (GameManager.current_stage == 3)
                newPosition = new Vector2(-20, 1);
            else if (GameManager.current_stage == 4)
                newPosition = new Vector2(-30, -15);

            spwn.transform.position = newPosition;
        }
        else if (GameManager.current_stage == 10 || GameManager.current_stage == 20)
        {
            spwn.transform.position = new Vector2(0, 0);
        }
        else
        {
            spwn.transform.position = new Vector2(0, 0);
        }
    }
}
    
