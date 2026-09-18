using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Vector3 tileSize = Vector3.one;
    public Vector3 TileSize => tileSize;
    [SerializeField] private GameObject asset;
  
}
