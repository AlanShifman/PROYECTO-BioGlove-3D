
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public Animator anim;
    public int points;

    [Header("Tipo de tacho")]
    public bool aceptaReciclable;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1. Algo entro en el trigger del tacho");

        Transform basura = other.transform;

        while (basura != null && !basura.CompareTag("Trash"))
        {
            basura = basura.parent;
        }

        if (basura == null)
            return;

        Debug.Log("2. El tacho detecto una basura");
        Debug.Log("TAG ENCONTRADO: " + basura.name);

        if (anim != null)
            anim.SetTrigger("Abrir");
        else
            Debug.LogWarning("El tacho no tiene Animator asignado");

        TrashType trashType = basura.GetComponent<TrashType>();

        if (trashType == null)
        {
            trashType = basura.GetComponentInChildren<TrashType>();
        }

        if (trashType == null)
        {
            Debug.LogWarning("3. La basura no tiene TrashType");
            return;
        }

        Debug.Log("3. TrashType encontrado. Reciclable: " + trashType.isRecyclable);

        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager != null)
        {
            if (trashType.isRecyclable == aceptaReciclable)
            {
                scoreManager.AddPoints(points);
                Debug.Log("4. Residuo colocado correctamente");
            }
            else
            {
                scoreManager.AddPoints(-Mathf.Abs(points));
                Debug.Log("4. Residuo colocado en el tacho incorrecto");
            }
        }

        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

        if (spawnManager != null)
        {
            spawnManager.TrashDestroyed();
        }

        Destroy(basura.gameObject);
        Debug.Log("PUNTOS PROCESADOS. DESTRUYENDO: " + basura.name);
    }
}
