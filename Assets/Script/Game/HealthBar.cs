using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Hp;


    void Update()   
    {
        Hp.fillAmount = GameManager.player_current_HP / GameManager.player_HP;
    }
}
