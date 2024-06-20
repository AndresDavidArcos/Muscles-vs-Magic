using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEnemyShuriken : MonoBehaviour
{
    public HudManager hudManager;
    public GameObject victoryCanvas;
    public float health = 300;
    void Start()
    {
        hudManager.UpdateHealthEnemyText("Vida del enemigo: " + health);
    }

    void Update()
    {
        
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        hudManager.UpdateHealthEnemyText("Vida del enemigo: " + health);
        if (health < 0)
        {
            Time.timeScale = 0;
            victoryCanvas.SetActive(true);
        }
    }
}
