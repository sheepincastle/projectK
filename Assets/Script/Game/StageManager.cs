using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //소환할 적과 위치를 세트로 받음
    //원하는 적과 위치를 순서를 맞춰서 배치
    public List<GameObject> enemies_phase1 = new List<GameObject>();
    public List<Transform> phase1_location = new List<Transform>();
    public List<GameObject> enemies_phase2 = new List<GameObject>();
    public List<Transform> phase2_location = new List<Transform>();
    public List<GameObject> enemies_phase3 = new List<GameObject>();
    public List<Transform> phase3_location = new List<Transform>();
    //소환된 적의 개수를 세서 다음 phase로 넘어가는 용도
    public List<GameObject> summoned_enemies = new List<GameObject>();
    public GameObject next_scene;
    public Transform portal_position;
    public int phase;
    public bool progress = true;
    public GameObject denger;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        summoned_enemies.RemoveAll(item => item == null);
        if(summoned_enemies.Count == 0 && progress)
        {
            switch(phase)
            {
                case 0:
                    progress = false;
                    for (int i = 0; i < enemies_phase1.Count; i++)//각 페이즈의 적 수만큼 반복
                    {
                        Instantiate(denger, phase1_location[i].position,transform.rotation);
                    }
                    Invoke("phase1", 3f);
                    return;
                case 1:
                    progress = false;
                    for (int i = 0; i < enemies_phase2.Count; i++)//각 페이즈의 적 수만큼 반복
                    {
                        Instantiate(denger, phase2_location[i].position, transform.rotation);
                    }
                    Invoke("phase2", 3f);
                    return;
                case 2:
                    progress = false;
                    for (int i = 0; i < enemies_phase3.Count; i++)//각 페이즈의 적 수만큼 반복
                    {
                        Instantiate(denger, phase3_location[i].position, transform.rotation);
                    }
                    Invoke("phase3", 3f);
                    return;
                case 3:
                    Instantiate(next_scene, portal_position.position, transform.rotation);
                    phase++;
                    return;
            }
        }
    }
    void phase1()
    {
        for (int i = 0; i < enemies_phase1.Count; i++)//각 페이즈의 적 수만큼 반복
        {
            summoned_enemies.Add(Instantiate(enemies_phase1[i], phase1_location[i].position, transform.rotation));//적 소환
        }
        phase++;
        progress = true;
        return;
    }
    void phase2()
    {
        for (int i = 0; i < enemies_phase2.Count; i++)//각 페이즈의 적 수만큼 반복
        {
            summoned_enemies.Add(Instantiate(enemies_phase2[i], phase2_location[i].position, transform.rotation));//적 소환
        }
        phase++;
        progress = true;
        return;
    }
    void phase3()
    {
        for (int i = 0; i < enemies_phase3.Count; i++)//각 페이즈의 적 수만큼 반복
        {
            summoned_enemies.Add(Instantiate(enemies_phase3[i], phase3_location[i].position, transform.rotation));//적 소환
        }
        phase++;
        progress = true;
        return;
    }
}
