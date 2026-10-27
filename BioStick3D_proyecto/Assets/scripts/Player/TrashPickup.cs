
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
            Transform trashObject = obj.transform;

            while (trashObject != null &&
                   !trashObject.CompareTag("Trash"))
            {
                trashObject = trashObject.parent;
            }

            if (trashObject == null)
                continue;

            Rigidbody rb = trashObject.GetComponent<Rigidbody>();

            if (rb == null)
                rb = trashObject.GetComponentInParent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogWarning("La basura no tiene Rigidbody.");
                continue;
            }

            if (holdPoint == null)
            {
                Debug.LogError("¡Falta asignar el Hold Point!");
                return;
            }

        heldTrash = rb.gameObject;

        // Evitar que la basura empuje al personaje
        Collider[] colliders = heldTrash.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
     {
        col.enabled = false;
     }

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

    Rigidbody rb = heldTrash.GetComponent<Rigidbody>();

    // Separar la basura del personaje y lanzarla hacia adelante
    heldTrash.transform.SetParent(null);
    heldTrash.transform.position =
        transform.position + transform.forward * 1f + Vector3.up * 1f;

    Collider[] colliders =
        heldTrash.GetComponentsInChildren<Collider>();

    foreach (Collider col in colliders)
    {
        col.enabled = true;
    }

    if (rb != null)
    {
        rb.isKinematic = false;
        rb.velocity = transform.forward * 6f + Vector3.up * 2f;
    }

    heldTrash = null;

    Debug.Log("¡Basura lanzada!");
}

}
