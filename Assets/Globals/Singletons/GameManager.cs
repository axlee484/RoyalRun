using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private int laneCount = 3;
    [SerializeField] private float platformWidth = 10f;
    [SerializeField] private int minimumSafeLanes = 1;
    public int MinimumSafeLanes => minimumSafeLanes;
    public int LaneCount => laneCount;
    public float PlatformWidth => platformWidth;
    private Vector3[] lanePostions;
    public IReadOnlyList<Vector3> LanePostions => Array.AsReadOnly(lanePostions);


    private void InitializeLanePositions()
    {
        
        lanePostions = new Vector3[laneCount];
        var initPos = Vector3.zero;


        var laneWidth = (float)platformWidth/laneCount;
        initPos.x = (float)laneWidth/2-(float)platformWidth/2;

        for(var i =0; i<laneCount; i++)
        {
            lanePostions[i] = initPos;
            initPos.x += laneWidth;
        }
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializeLanePositions();
    }
}
