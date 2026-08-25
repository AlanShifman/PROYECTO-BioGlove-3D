using UnityEngine;

public class Rat : MonoBehaviour
{
    public int puntosPerdidos = 5;

    private bool activada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activada)
        {
            activada = true;

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddPoints(-puntosPerdidos);
            }

            // Esperar antes de poder volver a afectar al jugador
            Invoke("Reactivar", 2f);
        }
    }

    private void Reactivar()
    {
        activada = false;
    }
}
