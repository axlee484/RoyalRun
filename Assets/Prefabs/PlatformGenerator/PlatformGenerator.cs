using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Chunk platformChunkPrefab;
    [SerializeField] private int chunkCount = 5;
    [SerializeField] private GameObject chunkParent;

    private void GeneratePlatform()
    {
        var zLength = platformChunkPrefab.AssetScale.z;
        var startPosZ = transform.position.z - (chunkCount/2)*zLength;
        var pos = Vector3.zero;
        pos.z = startPosZ;

        for(var i =0; i<chunkCount; i++)
        {
            var platform = Instantiate(platformChunkPrefab, pos, Quaternion.identity, chunkParent.transform);
            pos.z += zLength;
        }
    }
    private void Awake()
    {
        GeneratePlatform();
    }
}
