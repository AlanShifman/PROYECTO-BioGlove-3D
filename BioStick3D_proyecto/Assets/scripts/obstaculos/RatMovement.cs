using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public Transform[] puntos;
    public float velocidad = 2f;

    private int puntoActual = 0;
    private float altura;

    void Start()
    {
        // Guardamos la altura inicial de la rata
        altura = transform.position.y;
    }

    void Update()
    {
        if (puntos.Length == 0)
            return;

        Transform destino = puntos[puntoActual];

        // Creamos un destino manteniendo la altura de la rata
        Vector3 destinoPiso = new Vector3(
            destino.position.x,
            altura,
            destino.position.z
        );

        // Moverse hacia el punto
        transform.position = Vector3.MoveTowards(
            transform.position,
            destinoPiso,
            velocidad * Time.deltaTime
        );

        // Girar solamente sobre el eje Y
        Vector3 direccion = destinoPiso - transform.position;
        direccion.y = 0;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacion,
                5f * Time.deltaTime
            );
        }

        // Cuando llega al punto, pasar al siguiente
        if (Vector3.Distance(transform.position, destinoPiso) < 0.1f)
        {
            puntoActual++;

            // Volver al primero cuando termina los 12
            if (puntoActual >= puntos.Length)
            {
                puntoActual = 0;
            }
        }
    }
}