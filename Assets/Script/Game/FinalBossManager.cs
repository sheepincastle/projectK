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

        text.text = "���� ���� �������� ��ġ�� ������ ã�Ҵ�.";
        yield return new WaitForSeconds(3f);

        text.text = "������ �����߰� �츮�� ������ �������� �� �־���.";
        yield return new WaitForSeconds(3f);

        text.text = "�÷��� ���ּż� �����մϴ�!!";
        yield return new WaitForSeconds(3f);

        image.SetActive(true);
        script.SetActive(false);
    }
}
