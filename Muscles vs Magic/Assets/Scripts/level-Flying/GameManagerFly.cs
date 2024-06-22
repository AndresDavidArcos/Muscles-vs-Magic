using UnityEngine;
using TMPro;

public class GameManagerFly : MonoBehaviour
{
    public static GameManagerFly instanceFly; // Instancia única del GameManagerFly
    public TextMeshProUGUI winText; // Texto que mostrará el mensaje de victoria
    public TextMeshProUGUI sphereCounterText; // Texto que mostrará el contador de esferas recogidas
    public TextMeshProUGUI loseText; // Texto que mostrará el mensaje de derrota
    public TextMeshProUGUI timerText; // Texto que mostrará el tiempo restante
    public int sphereCount = 0; // Contador de esferas tocadas
    public int winCount = 10; // Número de esferas necesarias para ganar
    public float timeLimit = 60f; // Tiempo límite en segundos

    private float timeRemaining;
    private bool gameEnded = false;

    private void Awake()
    {
        // Asegura que solo haya una instancia del GameManagerFly
        if (instanceFly == null)
        {
            instanceFly = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Asegura que el juego no esté pausado al inicio
        Time.timeScale = 1;
        gameEnded = false;

        timeRemaining = timeLimit;
        UpdateTimer();

        Debug.Log("Game started. Time remaining: " + timeRemaining);
    }

    private void Update()
    {
        if (!gameEnded)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log("Time remaining: " + timeRemaining);
            UpdateTimer();

            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    // Llamar este método cada vez que se toca una esfera
    public void SphereTouched()
    {
        sphereCount++;
        UpdateSphereCounter();
        if (sphereCount >= winCount)
        {
            WinGame();
        }
    }

    // Actualizar el contador de esferas en la pantalla
    private void UpdateSphereCounter()
    {
        sphereCounterText.text = "Esferas recogidas: " + sphereCount;
    }

    // Actualizar el tiempo en la pantalla
    private void UpdateTimer()
    {
        timerText.text = "Tiempo: " + Mathf.Ceil(timeRemaining).ToString();
    }

    // Mostrar la pantalla de victoria y pausar el juego
    private void WinGame()
    {
        gameEnded = true;
        winText.gameObject.SetActive(true);
        Time.timeScale = 0; // Pausar el juego
    }

    // Mostrar la pantalla de derrota y pausar el juego
    private void GameOver()
    {
        gameEnded = true;
        loseText.gameObject.SetActive(true);
        Time.timeScale = 0; // Pausar el juego
    }
}
