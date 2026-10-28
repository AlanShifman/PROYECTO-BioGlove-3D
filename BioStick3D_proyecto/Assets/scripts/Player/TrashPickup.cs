
using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public Transform holdPoint;
    public float pickupRange = 3f;

    private GameObject heldTrash;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash == null)
                TryPickUp();
            else
                DropTrash();
        }
    }

    void TryPickUp()
    {
        Collider[] objects = Physics.OverlapSphere(
            transform.position + Vector3.up,
            pickupRange
        );

        foreach (Collider obj in objects)
        {
            Transform basura = obj.transform;

            while (basura != null &&
                   !basura.CompareTag("Trash"))
            {
                basura = basura.parent;
            }

            if (basura == null)
                continue;

            Rigidbody rb = basura.GetComponent<Rigidbody>();

            if (rb == null)
                rb = basura.GetComponentInParent<Rigidbody>();

            if (rb == null)
                continue;

            if (holdPoint == null)
            {
                Debug.LogError("¡Falta asignar el Hold Point!");
                return;
            }

            heldTrash = rb.gameObject;

            Collider[] colliders =
                heldTrash.GetComponentsInChildren<Collider>();

            foreach (Collider col in colliders)
                col.enabled = false;

            rb.isKinematic = true;

            heldTrash.transform.SetParent(holdPoint);
            heldTrash.transform.localPosition = Vector3.zero;
            heldTrash.transform.localRotation = Quaternion.identity;

            Debug.Log("¡Basura agarrada!");
            return;
        }

        Debug.Log("No se encontró basura cerca.");
    }

    void DropTrash()
    {
        if (heldTrash == null)
            return;

        GameObject basura = heldTrash;

        // Buscar el tacho más cercano.
        TrashBin[] tachos = FindObjectsOfType<TrashBin>();
        TrashBin tachoCercano = null;
        float distanciaMinima = 2.5f;

        foreach (TrashBin tacho in tachos)
        {
            float distancia = Vector3.Distance(
                transform.position,
                tacho.transform.position
            );

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                tachoCercano = tacho;
            }
        }

        // Soltar la basura.
        basura.transform.SetParent(null);

        Collider[] colliders =
            basura.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
            col.enabled = true;

        Rigidbody rb = basura.GetComponent<Rigidbody>();

        if (tachoCercano != null)
        {
            // Colocar la basura dentro de la zona del tacho.
            basura.transform.position =
                tachoCercano.transform.position + Vector3.up * 0.5f;

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
            }

            Debug.Log("Basura soltada cerca del tacho.");
        }
        else
        {
            // Si no hay tacho cerca, soltar normalmente.
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity =
                    transform.forward * 3f + Vector3.up;
            }

            Debug.Log("¡Basura soltada!");
        }

        heldTrash = null;
    }
}
