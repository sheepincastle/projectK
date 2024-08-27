using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door; // 문 오브젝트
    public float checkInterval = 0.5f; // 적을 체크하는 간격 (초)

    private float timeSinceLastCheck = 0f;

    private void Update()
    {
        // 일정 간격으로 적의 수를 체크
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck >= checkInterval)
        {
            CheckEnemies();
            timeSinceLastCheck = 0f;
        }
    }

    private void CheckEnemies()
    {
        // "Enemy" 태그를 가진 오브젝트들 검색
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 적이 하나도 없다면 문을 삭제
        if (enemies.Length == 0)
        {
            Destroy(door);
            // 적이 다 사라졌으므로 이 스크립트도 필요 없음
            this.enabled = false;
        }
    }
}

