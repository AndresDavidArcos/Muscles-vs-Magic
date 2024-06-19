using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText;

    public static GameManager Instance { get; private set; }
    public int rockAmmo = 12;

    public Text pointsText;
    public int points = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ammoText.text = rockAmmo.ToString();
        pointsText.text = points.ToString();
    }
}
