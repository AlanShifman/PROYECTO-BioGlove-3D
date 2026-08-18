using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public int points;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("¡La basura entró al tacho!");

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddPoints(points);
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