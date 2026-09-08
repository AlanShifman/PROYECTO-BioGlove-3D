using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 3f, -10);

    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // El offset gira junto con el Player
        Vector3 desiredPosition = target.TransformPoint(offset);

        // Movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.position = smoothedPosition;

        // La cámara mira al Player
        transform.LookAt(target);
    }
}