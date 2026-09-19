using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody body;
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private Bounds boundingBox;
    [SerializeField] private Animator animator;
    private int currentLaneIndex = 0;
    private Vector3[] lanePositions;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    
    private void Start()
    {
        lanePositions = new List<Vector3>(GameManager.Instance.LanePostions).ToArray();
        currentLaneIndex = GameManager.Instance.LaneCount/2;
        body.position = ClampVector3(GameManager.Instance.LanePostions[currentLaneIndex], boundingBox);
    }

    private void MoveToCurrentLaneIndex()
    {
        var targetPosition = lanePositions[currentLaneIndex];

        float newX = Mathf.MoveTowards(
            body.position.x,
            targetPosition.x,
            horizontalSpeed * Time.fixedDeltaTime
        );

        body.MovePosition(new Vector3(
            newX,
            body.position.y,
            body.position.z
        ));
    }
    private void MoveLeft()
    {
        if(currentLaneIndex == 0) return;
        currentLaneIndex--;
    }

    private void MoveRight()
    {
        if(currentLaneIndex == lanePositions.Length-1) return;
        currentLaneIndex++;
    }

    private void Jump()
    {
        // body.AddForce(Vector3.up*10, ForceMode.Impulse);
        // animator.SetTrigger("Jump");
    }
    public void Move(InputAction.CallbackContext moveData)
    {
        if(!moveData.performed) return;
        movement = moveData.ReadValue<Vector2>();
        if(movement.x>0) MoveRight();
        if(movement.x<0) MoveLeft();
        // if(movement.y>0) Jump();
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
        MoveToCurrentLaneIndex();
    }
}
