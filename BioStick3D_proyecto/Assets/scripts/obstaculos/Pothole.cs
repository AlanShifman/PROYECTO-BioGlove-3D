using UnityEngine;
using System.Collections;

public class Pothole : MonoBehaviour
{
    public int puntosPerdidos = 2;
    public float tiempoCaido = 1.5f;

    private bool cayendo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cayendo)
        {
            StartCoroutine(Caer(other.gameObject));
        }
    }

    IEnumerator Caer(GameObject jugador)
    {
        cayendo = true;

        // Restar 2 puntos
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            scoreManager.AddPoints(-puntosPerdidos);
        }

        // Desactivar movimiento
        PlayerMovement movimiento = jugador.GetComponent<PlayerMovement>();

        if (movimiento != null)
        {
            movimiento.enabled = false;
        }

        // Obtener Rigidbody
        Rigidbody rb = jugador.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;

            // Hacer que se incline y caiga
            rb.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
            rb.AddTorque(Vector3.right * 5f, ForceMode.Impulse);
        }

        // Esperar 1,5 segundos
        yield return new WaitForSeconds(tiempoCaido);

        // Frenar el Rigidbody
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        // Volver a permitir movimiento
        if (movimiento != null)
        {
            movimiento.enabled = true;
        }

        // IMPORTANTE:
        // Ya terminó la caída y puede volver a activarse
        cayendo = false;
    }
}