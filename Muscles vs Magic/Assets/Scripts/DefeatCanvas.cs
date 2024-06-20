using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefeatCanvas : MonoBehaviour
{
    public Button repeatButton;

    void Start()
    {
        if (repeatButton != null)
        {
            repeatButton.onClick.AddListener(OnRepeatButtonClicked);
        }
        else
        {
            Debug.LogWarning("Button 'Repeat' no asignado.");
        }
    }

    void OnRepeatButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
}
