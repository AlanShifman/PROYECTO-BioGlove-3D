using UnityEngine;

public class TrashPickup : MonoBehaviour
{
    public Transform holdPoint;

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
        Collider[] objects = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider obj in objects)
        {
            if (obj.CompareTag("Trash"))
            {
                heldTrash = obj.gameObject;

                heldTrash.GetComponent<Rigidbody>().isKinematic = true;
                heldTrash.transform.SetParent(holdPoint);
                heldTrash.transform.localPosition = Vector3.zero;

                break;
            }
        }
    }

    void DropTrash()
    {
        heldTrash.GetComponent<Rigidbody>().isKinematic = false;
        heldTrash.transform.SetParent(null);

        heldTrash = null;
    }
}