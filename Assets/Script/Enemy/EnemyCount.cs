using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text enemyCountText; // UI Text 컴포넌트를 연결하세요.

    void Start()
    {
        UpdateEnemyCount(); // 게임 시작 시 초기 적의 수를 업데이트
    }

    void Update()
    {
        UpdateEnemyCount(); // 매 프레임마다 적의 수를 업데이트
    }

    // 적의 수를 업데이트하는 메서드
    void UpdateEnemyCount()
    {
        // 'enemy' 태그를 가진 모든 게임 오브젝트를 찾습니다.
        int enemyCount = GameObject.FindGameObjectsWithTag("enemy").Length;

        // 적의 수를 UI 텍스트에 표시합니다.
        enemyCountText.text = "Enemy Count: " + enemyCount;
    }
}

