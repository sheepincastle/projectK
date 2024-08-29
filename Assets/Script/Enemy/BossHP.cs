using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour
{
    public EnemyData boss_data;
    public Image HP;

    void Update()
    {
        HP.fillAmount = (float)boss_data.enemy_current_HP/boss_data.enemy_HP;
    }
}
