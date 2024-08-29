using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FinalBossManager : MonoBehaviour
{
    public GameObject image;
    public Text text;
    public GameObject script;
    bool progress = true;

    // Start is called before the first frame update
    void Start()
    {
        image.SetActive(false);
        script.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0 && progress)
        {
            StartCoroutine(End());
            progress = false;
        }
    }
    IEnumerator End()
    {
        script.SetActive(true);

        text.text = "나는 힘든 여정끝에 납치된 동생을 찾았다.";
        yield return new WaitForSeconds(3f);

        text.text = "동생은 무사했고 우리는 무사히 빠져나올 수 있었다.";
        yield return new WaitForSeconds(3f);

        text.text = "플레이 해주셔서 감사합니다!!";
        yield return new WaitForSeconds(3f);

        image.SetActive(true);
        script.SetActive(false);
    }
}
