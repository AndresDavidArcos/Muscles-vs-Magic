using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text ammoText;
    public Text pointsText;

    public GameObject victoryCanvas;
    public GameObject defeatCanvas;

    public static GameManager Instance { get; private set; }
    public int totalAmmo = 64;
    public int rockAmmo = 12;
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

    public void ShowDefeatCanvas()
    {
        defeatCanvas.SetActive(true);
    }

    public void ShowVictoryCanvas()
    {
        victoryCanvas.SetActive(true);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
