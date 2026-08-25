using UnityEngine;
using System.Collections;

public class ToxicPuddle : MonoBehaviour
{
    public float tiempoInmovilizado = 3f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;
            StartCoroutine(InmovilizarJugador(other.gameObject));
        }
    }

    IEnumerator InmovilizarJugador(GameObject jugador)
    {
        PlayerMovement movimiento = jugador.GetComponent<PlayerMovement>();

        if (movimiento != null)
        {
            movimiento.enabled = false;

            yield return new WaitForSeconds(tiempoInmovilizado);

            movimiento.enabled = true;
        }

        activado = false;
    }
}