using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public int points;

    [Header("Tipo de tacho")]
    public bool aceptaReciclable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            TrashType trashType = other.GetComponent<TrashType>();

            if (trashType == null)
            {
                Debug.LogWarning("Esta basura no tiene TrashType.");
                return;
            }

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                if (trashType.isRecyclable == aceptaReciclable)
                {
                    // Tipo correcto
                    scoreManager.AddPoints(points);

                    Debug.Log("¡Residuo colocado correctamente!");
                }
                else
                {
                    // Tipo incorrecto
                    scoreManager.AddPoints(-Mathf.Abs(points));

                    Debug.Log("¡Residuo colocado en el tacho incorrecto!");
                }
            }

            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();

            if (spawnManager != null)
            {
                spawnManager.TrashDestroyed();
            }

            Destroy(other.gameObject);
        }
    }
}