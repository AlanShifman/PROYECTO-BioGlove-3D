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
    int totalPoints = spawnPointsParent.childCount;

    if (amountToSpawn > totalPoints)
    {
        amountToSpawn = totalPoints;
    }

    for (int i = 0; i < amountToSpawn; i++)
    {
        Transform spawnPoint = spawnPointsParent.GetChild(i);

        Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);
    }
}
}