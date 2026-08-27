using UnityEngine;

public class Rat : MonoBehaviour
{
    [Header("Puntos")]
    public int puntosPerdidos = 5;

    [Header("Movimiento")]
    public float velocidad = 1.5f;
    public float velocidadGiro = 180f;

    public float tiempoMinimoCambio = 2f;
    public float tiempoMaximoCambio = 5f;

    public float tiempoMinimoParada = 0.5f;
    public float tiempoMaximoParada = 1.5f;

    private bool activada = false;

    private float tiempoCambio;
    private float proximoCambio;

    private bool quieta = false;
    private float tiempoParada;

    private void Start()
    {
        ElegirNuevoTiempo();
    }

    private void Update()
    {
        Movimiento();
    }

    private void Movimiento()
    {
        if (quieta)
        {
            tiempoParada -= Time.deltaTime;

            if (tiempoParada <= 0)
            {
                quieta = false;
                ElegirNuevoTiempo();
            }

            return;
        }

        // Avanza suavemente
        transform.position += transform.forward * velocidad * Time.deltaTime;

        tiempoCambio += Time.deltaTime;

        // Cada cierto tiempo cambia de dirección
        if (tiempoCambio >= proximoCambio)
        {
            Girar();
        }
    }

    private void Girar()
    {
        tiempoCambio = 0;

        // A veces gira hacia un lado y a veces hacia el otro
        float giro = Random.Range(-90f, 90f);

        transform.Rotate(0, giro, 0);

        // Hay una posibilidad de que se quede quieta
        if (Random.Range(0f, 1f) < 0.25f)
        {
            quieta = true;
            tiempoParada = Random.Range(tiempoMinimoParada, tiempoMaximoParada);
        }

        ElegirNuevoTiempo();
    }

    private void ElegirNuevoTiempo()
    {
        proximoCambio = Random.Range(tiempoMinimoCambio, tiempoMaximoCambio);
    }

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

            Invoke("Reactivar", 2f);
        }
    }

    private void Reactivar()
    {
        activada = false;
    }
}