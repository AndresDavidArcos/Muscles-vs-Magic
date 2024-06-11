using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public Text health;

    public void UpdateHealthText(string healthText)
    {
        health.text = healthText;
    }
}
