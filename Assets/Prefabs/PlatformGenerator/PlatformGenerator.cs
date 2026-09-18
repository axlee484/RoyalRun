using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Chunk platformChunkPrefab;
    [SerializeField] private int chunkCount = 5;
    [SerializeField] private GameObject chunkParent;
    [SerializeField] private float moveSpeed = 10f;
    public float MoveSpeed => moveSpeed;
    public void SetMoveSpeed(float value) => moveSpeed = value;
    private readonly List<Chunk> chunksContainer = new();
    private Vector3 startPosition;


    private void MoveChunk()
    {
        for(var i =0; i<chunksContainer.Count; i++)
        {
            var chunk = chunksContainer[i];
            chunk.transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.back);
            if(chunk.transform.position.z < Camera.main.transform.position.z)
            {
                RemoveChunk(chunk);
                GenerateNewChunk(startPosition);
            }
        }
    }

    private void RemoveChunk(Chunk chunk)
    {
        chunksContainer.Remove(chunk);
        Destroy(chunk.gameObject);
    }

    private void GenerateNewChunk(Vector3 position)
    {
        var chunk = Instantiate(platformChunkPrefab, position, Quaternion.identity, chunkParent.transform);
        chunksContainer.Add(chunk);
    }
    private void GenerateChunks()
    {
        var zLength = platformChunkPrefab.TileSize.z;
        var startPosZ = transform.position.z + (chunkCount/2)*zLength;

        startPosition.z = startPosZ;
        startPosition.y -= platformChunkPrefab.TileSize.y;
        var pos = startPosition;

        for(var i =0; i<chunkCount; i++)
        {
            GenerateNewChunk(pos);
            pos.z -= zLength;
        }
    }
    private void Awake()
    {
        GenerateChunks();
    }

    private void FixedUpdate()
    {
        MoveChunk();
    }
}
