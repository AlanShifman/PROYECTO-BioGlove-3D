using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject trashPrefab;
    public Transform spawnPointsParent;

    public int amountToSpawn = 10;

    private int currentTrash = 0;

    void Start()
    {
        SpawnInitialTrash();
    }

    void SpawnInitialTrash()
    {
        int totalPoints = spawnPointsParent.childCount;

        if (amountToSpawn > totalPoints)
        {
            amountToSpawn = totalPoints;
        }

        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnOneTrash();
        }
    }

    void SpawnOneTrash()
    {
        int totalPoints = spawnPointsParent.childCount;

        if (totalPoints == 0)
        {
            Debug.LogWarning("No hay SpawnPoints.");
            return;
        }

        int randomIndex = Random.Range(0, totalPoints);

        Transform spawnPoint = spawnPointsParent.GetChild(randomIndex);

        Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);

        currentTrash++;
    }

    public void TrashDestroyed()
    {
        currentTrash--;

        if (currentTrash < amountToSpawn)
        {
            SpawnOneTrash();
        }
    }
}