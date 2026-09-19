using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int laneCount = 3;
    [SerializeField] private float platformWidth = 10f;
    public int LaneCount => laneCount;
    public float PlatformWidth => platformWidth;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
