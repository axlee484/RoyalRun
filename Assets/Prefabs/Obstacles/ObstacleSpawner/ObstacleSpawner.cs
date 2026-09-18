using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private BaseObstacle[] obstaclePrefabs;
    [SerializeField] private GameObject obstacleContainer;
    [SerializeField] private float horizontalAcceleration = 9.8f;
    private Bounds spawnArea;
    [SerializeField] private float spawnTime = 2f;
    private void Awake()
    {
        var spawnBox = GetComponent<BoxCollider>();
        spawnArea = spawnBox.bounds;
    }

    private void Start()
    {
        StartCoroutine(SpawnObstacleCoroutine());
    }

    private Vector3 GetRandomPointVector3(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        var z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x,y,z);
    }
    private void SpawnObstacle(BaseObstacle obstaclePrefab)
    {
        var spawnPoint = GetRandomPointVector3(spawnArea);
        var obstacle = Instantiate(obstaclePrefab, spawnPoint, Random.rotation, obstacleContainer.transform);
        obstacle.ApplyHorizontalForce(horizontalAcceleration);
    }


    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            var randId = Random.Range(0, obstaclePrefabs.Length);
            var prefab = obstaclePrefabs[randId];
            SpawnObstacle(prefab);
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
