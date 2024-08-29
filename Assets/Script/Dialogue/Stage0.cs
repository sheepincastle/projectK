using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage0 : MonoBehaviour
{
    StageManager stageManager;
    public GameObject script;
    public Text text;

    void Start()
    {
        stageManager = GetComponent<StageManager>();
        //튜토리얼 스크립트 출력 전 일시정지
        stageManager.progress = false;
        PlayerMove.moveable = false;
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        script.SetActive(true);

        text.text = "균열을 타고 들어오니까 갑자기 미래로 와 버렸어...";
        PlayerMove.moveable = true;
        yield return new WaitForSeconds(3f);
        
        text.text = " 낯선 사람들에게 납치된 내 동생을 찾아야 해!";
        PlayerMove.moveable = true;
        yield return new WaitForSeconds(3f);
        
        text.text = "'w a s d'를 통해 움직일 수 있어.";
        PlayerMove.moveable = true;
        yield return new WaitForSeconds(3f);

        text.text = "'tap키'를 통해 무기를 활로 바꿀 수 있어.";
        yield return new WaitForSeconds(3f);

        stageManager.progress = true;//적 소환
        text.text = "으... 쥐라니, 저런걸 보면 '상태이상'에 빠지고 말거야.";
        yield return new WaitForSeconds(3f);

        text.text = "쥐에 부딪히면 '경직'에 걸릴 수도 있어. 조심해.";
        yield return new WaitForSeconds(3f);

        script.SetActive(false);
    }
}
