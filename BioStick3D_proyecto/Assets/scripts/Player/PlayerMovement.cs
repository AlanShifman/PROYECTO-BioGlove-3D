using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;

    [Header("Rotación")]
    public float rotationSpeed = 8f;

    private Rigidbody rb;

    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(horizontal, 0f, vertical);

        // Evita que la velocidad diagonal sea mayor
        input = Vector3.ClampMagnitude(input, 1f);

        if (input.magnitude > 0.01f)
        {
            // -------------------------
            // MOVIMIENTO SUAVE
            // -------------------------

            Vector3 targetVelocity = input * speed;

            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );

            // -------------------------
            // ROTACIÓN SUAVE
            // -------------------------

            Quaternion targetRotation = Quaternion.LookRotation(input);

            Quaternion smoothRotation = Quaternion.RotateTowards(
                rb.rotation,
                targetRotation,
                rotationSpeed * 100f * Time.fixedDeltaTime
            );

            rb.MoveRotation(smoothRotation);
        }
        else
        {
            // Cuando soltamos las teclas,
            // desacelera suavemente
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        // -------------------------
        // APLICAR MOVIMIENTO
        // -------------------------

        rb.MovePosition(
            rb.position + currentVelocity * Time.fixedDeltaTime
        );
    }
}