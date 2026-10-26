
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
            {
                TryPickUp();
            }
            else
            {
                DropTrash();
            }
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
            if (obj.CompareTag("Trash"))
            {
                Rigidbody rb = obj.GetComponentInParent<Rigidbody>();

                if (rb == null)
                {
                    Debug.LogWarning("La basura no tiene Rigidbody.");
                    continue;
                }

                heldTrash = rb.gameObject;

                rb.isKinematic = true;
                heldTrash.transform.SetParent(holdPoint);
                heldTrash.transform.localPosition = Vector3.zero;
                heldTrash.transform.localRotation = Quaternion.identity;

                Debug.Log("¡Basura agarrada!");
                return;
            }
        }

        Debug.Log("No se encontró basura cerca.");
    }

    void DropTrash()
    {
        Rigidbody rb = heldTrash.GetComponent<Rigidbody>();

        heldTrash.transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        heldTrash = null;
    }
}
