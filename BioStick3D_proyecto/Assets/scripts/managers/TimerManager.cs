
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;
    public ScoreManager scoreManager;

    public GameObject scoreBackground;
    public GameObject timeBackground;

    public GameObject scoreTextObject;
    public GameObject timerTextObject;

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

            scoreManager.ShowFinalScore();

            scoreBackground.SetActive(false);
            timeBackground.SetActive(false);

            scoreTextObject.SetActive(false);
            timerTextObject.SetActive(false);

            gameOverPanel.SetActive(true);

            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenúInicial");
    }
}
