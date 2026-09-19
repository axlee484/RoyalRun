using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Vector3 tileSize = Vector3.one;
    [SerializeField] private Pickable[] pickablePrefabs;
    [SerializeField] private BaseObstacle[] obstaclePrefabs;
    public Vector3 TileSize => tileSize;
    [SerializeField] private GameObject asset;
    private Vector3[] lanePositions;
    private int minimumSafeLanes;


    private int[] PickRandomPositions(int obstacleCount)
    {
        var randomLanes = new List<int>();
        for(var i =0; i<lanePositions.Length; i++)
        {
            randomLanes.Add(i);
        }

        for(var i = 0; i < obstacleCount; i++)
        {
            var randomIndex = Random.Range(i, randomLanes.Count);
            (randomLanes[^1], randomLanes[randomIndex]) = (randomLanes[randomIndex], randomLanes[^1]);
        }
        return randomLanes.ToArray();
    }
    private void SpawnObstacles(int[] obstacleLanes)
    {
        var prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];


        for(var i =0; i<obstacleLanes.Length; i++)
        {
            var point = asset.transform.TransformPoint(lanePositions[obstacleLanes[i]]);
            Instantiate(prefab,point,Quaternion.identity, asset.transform);
        }
    }

    private void SpawnPickables(int[] pickableLanes)
    {
       var pickableCount = Random.Range(0, pickableLanes.Length);
       for(var i =0; i<pickableCount; i++)
        {
            var pickablePrefab = pickablePrefabs[Random.Range(0, pickablePrefabs.Length)];
            var point = asset.transform.TransformPoint(lanePositions[pickableLanes[i]]);
            Instantiate(pickablePrefab,point,Quaternion.identity, asset.transform);
        }
    }

    private void CreateSpawnables()
    {
        var obstacleCount = Random.Range(0, lanePositions.Length-minimumSafeLanes+1);
        var randomLanes = PickRandomPositions(obstacleCount);
        var obstacleLanes = randomLanes[..obstacleCount];
        var pickupLanes = randomLanes[obstacleCount..];


        SpawnObstacles(obstacleLanes);
        SpawnPickables(pickupLanes);
    }


    private void Start()
    {
        lanePositions = GameManager.Instance.LanePostions.ToArray();
        minimumSafeLanes = GameManager.Instance.MinimumSafeLanes;
        CreateSpawnables();
    }

}
