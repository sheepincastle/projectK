using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door; // 문 오브젝트
    public float checkInterval = 0.5f; // 적을 체크하는 간격 (초)
    public float timeToDestroyDoor = 4f; // 적이 없어진 후 기다릴 시간 (초)

    private float timeSinceLastCheck = 0f; // 마지막 체크 이후 경과 시간
    private float timeWithoutEnemies = 0f; // 적이 없어진 후 경과 시간
    private bool enemiesDetected = false; // 적이 감지되었는지 여부

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

        if (enemies.Length == 0)
        {
            // 적이 없을 경우 시간을 증가시킴
            timeWithoutEnemies += checkInterval;

            // 4초 동안 적이 없으면 문을 삭제
            if (timeWithoutEnemies >= timeToDestroyDoor)
            {
                Destroy(door);
                // 적이 다 사라졌으므로 이 스크립트도 필요 없음
                this.enabled = false;
            }
        }
        else
        {
            // 적이 다시 나타났다면 카운트를 초기화
            timeWithoutEnemies = 0f;
        }
    }
}
