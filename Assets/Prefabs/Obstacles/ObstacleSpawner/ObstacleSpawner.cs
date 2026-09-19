using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<BaseObstacle> obstaclePrefabs = new();
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
        if(obstaclePrefabs.Count ==0) return;
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
        if(obstaclePrefab == null) return;
        var spawnPoint = GetRandomPointVector3(spawnArea);
        var obstacle = Instantiate(obstaclePrefab, spawnPoint, Random.rotation, obstacleContainer.transform);
        obstacle.SetHorizontalAcceleration(horizontalAcceleration);
    }


    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            var randId = Random.Range(0, obstaclePrefabs.Count);
            var prefab = obstaclePrefabs[randId];
            SpawnObstacle(prefab);
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
