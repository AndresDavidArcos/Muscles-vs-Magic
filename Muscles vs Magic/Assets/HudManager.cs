using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text health;
    public Text energy;
    public Text shurikens;
    public Text healthEnemy;

    public void UpdateHealthText(string healthText)
    {
        health.text = healthText;
    }


    public void UpdateHealthEnemyText(string healthText)
    {
        healthEnemy.text = healthText;
    }

    public void UpdateEnergyText(string energyText)
    {
        energy.text = energyText;
    }

    public void UpdateShurikensText(string shurikenText)
    {
        shurikens.text = shurikenText;
    }
}
