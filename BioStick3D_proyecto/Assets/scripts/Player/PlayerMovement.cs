using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;

    [Header("Rotación")]
    public float rotationSpeed = 120f;

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

        // AVANZAR / RETROCEDER

        Vector3 direction = transform.forward * vertical;

        Vector3 targetVelocity = direction * speed;

        if (Mathf.Abs(vertical) > 0.01f)
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.fixedDeltaTime
            );
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        // GIRAR IZQUIERDA / DERECHA

        if (Mathf.Abs(horizontal) > 0.01f)
        {
            float rotation = horizontal * rotationSpeed * Time.fixedDeltaTime;

            Quaternion turn = Quaternion.Euler(
                0f,
                rotation,
                0f
            );

            rb.MoveRotation(rb.rotation * turn);
        }

        // APLICAR MOVIMIENTO

        rb.MovePosition(
            rb.position + currentVelocity * Time.fixedDeltaTime
        );
    }
}