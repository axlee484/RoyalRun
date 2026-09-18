using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Vector3 assetScale = Vector3.one;
    public Vector3 AssetScale => assetScale;
    [SerializeField] private Vector3 assetOffset = Vector3.zero;
    [SerializeField] private GameObject asset;
    private void Awake()
    {
        asset.transform.localScale = assetScale;
        asset.transform.localPosition = assetOffset;
    }
}
