using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool gameOver = false;

    void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameOver)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            int seconds = Mathf.CeilToInt(timeRemaining);

            timerText.text = "Tiempo: " + seconds;
        }
        else
        {
            timeRemaining = 0;
            timerText.text = "Tiempo: 0";

            gameOver = true;

            gameOverPanel.SetActive(true);

            Time.timeScale = 0;
        }
    }
    public void RestartGame()
{
    Time.timeScale = 1;
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
    );
}
public void BackToMenu()
{
    Time.timeScale = 1;
    UnityEngine.SceneManagement.SceneManager.LoadScene("MenúInicial");
}
}