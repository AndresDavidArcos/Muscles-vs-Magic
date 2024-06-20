using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicEnemyShuriken : MonoBehaviour
{
    public HudManager hudManager;
    public float health = 300;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        hudManager.UpdateHealthEnemyText("Vida del enemigo: " + health);
        if (health < 0)
        {
            print("Has ganado");
        }
    }
}
