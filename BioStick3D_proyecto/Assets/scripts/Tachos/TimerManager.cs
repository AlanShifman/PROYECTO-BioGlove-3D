using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;

    void Update()
    {
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

            gameOverText.gameObject.SetActive(true);

            Time.timeScale = 0;
        }
    }
}