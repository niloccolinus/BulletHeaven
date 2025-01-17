using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private BoxCollider spawnArea;
    [SerializeField]
    private float height = 1f;
    [SerializeField]
    [Range(0.1f, 2.0f)]
    private float spawnDelay = 2.0f;

    void Start()
    {
        InvokeRepeating("InstantiateEnemy", 2.0f, spawnDelay);
    }

    private void InstantiateEnemy()
    {
        Bounds bounds = spawnArea.bounds;

        // generate random positions in bounds limits
        float randomPosX = Random.Range(bounds.min.x, bounds.max.x);
        float randomPosZ = Random.Range(bounds.min.z, bounds.max.z);

        Vector3 randomPosition = new Vector3(randomPosX, height, randomPosZ);

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }


}
