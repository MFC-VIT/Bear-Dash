using UnityEngine;

public class GroundTile : MonoBehaviour {

    GroundSpawner groundSpawner;

    private void Start () {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        SpawnObstacle();
    }

    private void OnTriggerExit (Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2);
    }

    public GameObject obstaclePrefab1;
    public GameObject obstaclePrefab2;

    public void SpawnObstacle ()
    {
        // Create an array with the obstacle prefabs
        GameObject[] obstaclePrefabs = { obstaclePrefab1, obstaclePrefab2 };

        // Randomly select an obstacle prefab from the array
        GameObject selectedObstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Randomly select a spawn point index
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Instantiate the selected prefab at the spawn point
        Instantiate(selectedObstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}