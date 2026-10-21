
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public Transform spawnPointsParent;

    public int amountToSpawn = 15;

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

        if (totalPoints == 0 || trashPrefabs.Length == 0)
        {
            Debug.LogWarning("Faltan SpawnPoints o prefabs de basura.");
            return;
        }

        int randomPointIndex = Random.Range(0, totalPoints);
        int randomTrashIndex = Random.Range(0, trashPrefabs.Length);

        Transform spawnPoint = spawnPointsParent.GetChild(randomPointIndex);

        GameObject selectedTrash = trashPrefabs[randomTrashIndex];

        Instantiate(selectedTrash, spawnPoint.position, Quaternion.identity);

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
