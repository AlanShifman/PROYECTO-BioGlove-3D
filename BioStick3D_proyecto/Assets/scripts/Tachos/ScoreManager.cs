using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public void AddPoints(int points)
    {
        score += points;

        Debug.Log("Puntaje: " + score);
    }
}