using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody body;
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private Bounds boundingBox;
    private Vector3[] lanePostions;
    private int currentLaneIndex = 0;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void InitializeLanePositions()
    {
        var laneCount = GameManager.Instance.LaneCount;
        var platformWidth = GameManager.Instance.PlatformWidth;
        lanePostions = new Vector3[laneCount];
        var initPos = Vector3.zero;


        var laneWidth = (float)platformWidth/laneCount;
        initPos.x = (float)laneWidth/2-(float)platformWidth/2;

        for(var i =0; i<laneCount; i++)
        {
            lanePostions[i] = initPos;
            initPos.x += laneWidth;
        }
        currentLaneIndex = laneCount/2;
    }
    private void Start()
    {
        InitializeLanePositions();
        body.position = ClampVector3(lanePostions[currentLaneIndex], boundingBox);
    }

    private void MoveLeft()
    {
        if(currentLaneIndex == 0) return;
        currentLaneIndex--;

    }
    public void Move(InputAction.CallbackContext moveData)
    {
        movement = moveData.ReadValue<Vector2>();
    }

    private Vector3 ClampVector3(Vector3 vector3, Bounds bounds)
    {
        vector3.x = Mathf.Clamp(vector3.x, bounds.min.x, bounds.max.x);
        vector3.y = Mathf.Clamp(vector3.y, bounds.min.y, bounds.max.y);
        vector3.z = Mathf.Clamp(vector3.z, bounds.min.z, bounds.max.z);
        return vector3;
    }
    private void FixedUpdate()
    {
        if(movement == Vector2.zero) return;
        var xDirection = Math.Sign(movement.x);
        var newPosition = body.position + horizontalSpeed * Time.fixedDeltaTime * xDirection * Vector3.right;
        newPosition = ClampVector3(newPosition, boundingBox);
        body.MovePosition(newPosition);
    }
}
