using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject trashPrefab;

    public Transform spawnPointsParent;

    public int amountToSpawn = 5;

    void Start()
    {
        SpawnTrash();
    }

    void SpawnTrash()
    {
        int spawned = 0;

        while (spawned < amountToSpawn)
        {
            int randomIndex = Random.Range(0, spawnPointsParent.childCount);

            Transform spawnPoint = spawnPointsParent.GetChild(randomIndex);

            Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);

            spawned++;
        }
    }
}