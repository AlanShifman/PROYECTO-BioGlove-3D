
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    public void AddPoints(int points)
    {
        score += points;

        Debug.Log("Puntaje: " + score);

        scoreText.text = "Puntaje: " + score;
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = score.ToString();
    }
}
