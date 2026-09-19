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
    private void SpawnObstacles()
    {
        var obstacleCount = Random.Range(0, lanePositions.Length-minimumSafeLanes+1);
        var obstacleLanes = PickRandomPositions(obstacleCount);
        var prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];


        for(var i =0; i<obstacleCount; i++)
        {
            var point = asset.transform.TransformPoint(lanePositions[obstacleLanes[i]]);
            Instantiate(prefab,point,Quaternion.identity, asset.transform);
        }
    }

    private void SpawnPickables()
    {
       
    }


    private void Start()
    {
        lanePositions = GameManager.Instance.LanePostions.ToArray();
        minimumSafeLanes = GameManager.Instance.MinimumSafeLanes;
        SpawnObstacles();
    }

}
