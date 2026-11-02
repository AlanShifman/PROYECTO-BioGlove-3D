
using UnityEngine;
using System.Collections;

public class TrashPickup : MonoBehaviour
{
    public Transform holdPoint;
    public float pickupRange = 3f;
    public Animator anim;

    private GameObject heldTrash;
    private bool pickingUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash == null && !pickingUp)
                TryPickUp();
            else if (heldTrash != null)
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

            pickingUp = true;

            if (anim != null)
                anim.SetTrigger("Pickup");

            StartCoroutine(AgarrarDespues(rb.gameObject));

            return;
        }

        Debug.Log("No se encontró basura cerca.");
    }

    IEnumerator AgarrarDespues(GameObject basura)
    {
        yield return new WaitForSeconds(1f);

        if (basura == null)
        {
            pickingUp = false;
            yield break;
        }

        heldTrash = basura;

        Collider[] colliders =
            heldTrash.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
            col.enabled = false;

        Rigidbody rb = heldTrash.GetComponent<Rigidbody>();

        rb.isKinematic = true;

        heldTrash.transform.SetParent(holdPoint);
        heldTrash.transform.localPosition = Vector3.zero;
        heldTrash.transform.localRotation = Quaternion.identity;

        pickingUp = false;

        Debug.Log("¡Basura agarrada!");
    }

    void DropTrash()
    {
        if (heldTrash == null)
            return;

        GameObject basura = heldTrash;

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

        basura.transform.SetParent(null);

        Collider[] colliders =
            basura.GetComponentsInChildren<Collider>();

        foreach (Collider col in colliders)
            col.enabled = true;

        Rigidbody rb = basura.GetComponent<Rigidbody>();

        if (tachoCercano != null)
        {
            basura.transform.position =
                tachoCercano.transform.position + Vector3.up * 0.5f;

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = false;
            }
        }
        else
        {
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity =
                    transform.forward * 3f + Vector3.up;
            }
        }

        heldTrash = null;
    }
}
