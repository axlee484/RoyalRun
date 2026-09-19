using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Chunk platformChunkPrefab;
    [SerializeField] private int chunkCount = 5;
    [SerializeField] private GameObject chunkParent;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float destructionOffset = 10f;
    public float MoveSpeed => moveSpeed;
    public void SetMoveSpeed(float value) => moveSpeed = value;
    private readonly List<Chunk> chunksContainer = new();
    private Vector3 startPosition;
    private Chunk lastChunk;
    private float zLength;


    private void MoveChunk()
    {
        for(var i =0; i<chunksContainer.Count; i++)
        {
            var chunk = chunksContainer[i];
            chunk.transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.back);
            if(chunk.transform.position.z < Camera.main.transform.position.z - destructionOffset)
            {
                RemoveChunk(chunk);
                GenerateNewChunk();
            }
        }
    }

    private void RemoveChunk(Chunk chunk)
    {
        chunksContainer.Remove(chunk);
        Destroy(chunk.gameObject);
    }

    private void GenerateNewChunk()
    {
        var newPosition = lastChunk != null? lastChunk.transform.position : Vector3.zero;
        newPosition.z += zLength;
        var chunk = Instantiate(platformChunkPrefab, newPosition, Quaternion.identity, chunkParent.transform);
        chunksContainer.Add(chunk);
        lastChunk = chunk;
    }
    private void GenerateChunks()
    {
        
        var startPosZ = transform.position.z + (chunkCount/2)*zLength;
        startPosition.z = startPosZ;
        var pos = startPosition;

        for(var i =0; i<chunkCount; i++)
        {
            GenerateNewChunk();
            pos.z -= zLength;
        }
    }
    private void Awake()
    {
        zLength = platformChunkPrefab.TileSize.z;
        GenerateChunks();
    }

    private void FixedUpdate()
    {
        MoveChunk();
    }
}
