using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody body;

    private Vector2 movement;

    private bool isJumping;
    public bool IsJumping => isJumping;

    [Header("Movement")]
    [SerializeField] private float laneChangeSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;

    [Header("References")]
    [SerializeField] private Bounds boundingBox;
    [SerializeField] private Animator animator;

    private int currentLaneIndex;
    private Vector3[] lanePositions;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();

        // Player should not be moved sideways by collisions.
        // Y remains free for jumping/gravity.
        body.constraints =
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        lanePositions = new List<Vector3>(
            GameManager.Instance.LanePostions
        ).ToArray();

        currentLaneIndex = GameManager.Instance.LaneCount / 2;

        Vector3 startPosition =
            GameManager.Instance.LanePostions[currentLaneIndex];

        body.position = ClampVector3(startPosition, boundingBox);
    }

    private void FixedUpdate()
    {
        MoveToCurrentLane();
    }

    private void MoveToCurrentLane()
    {
        float targetX = lanePositions[currentLaneIndex].x;

        float difference = targetX - body.position.x;

        float xVelocity = difference * laneChangeSpeed;

        // Stop tiny movements around the target.
        if (Mathf.Abs(difference) < 0.01f)
        {
            xVelocity = 0f;
        }

        Vector3 velocity = body.linearVelocity;

        // We control ONLY X.
        // Y is left completely to physics.
        velocity.x = xVelocity;

        body.linearVelocity = velocity;
    }

    private void MoveLeft()
    {
        if (currentLaneIndex <= 0)
            return;

        currentLaneIndex--;
    }

    private void MoveRight()
    {
        if (currentLaneIndex >= lanePositions.Length - 1)
            return;

        currentLaneIndex++;
    }

    private void Jump()
    {
        if (isJumping)
            return;

        isJumping = true;

        body.AddForce(
            Vector3.up * jumpForce,
            ForceMode.Impulse
        );

        animator.SetTrigger("Jump");
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        movement = context.ReadValue<Vector2>();

        if (movement.x > 0)
        {
            MoveRight();
        }
        else if (movement.x < 0)
        {
            MoveLeft();
        }

        if (movement.y > 0)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Simple version: touching something below means landed.
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isJumping = false;
                break;
            }
        }
    }

    private Vector3 ClampVector3(Vector3 vector3, Bounds bounds)
    {
        vector3.x = Mathf.Clamp(
            vector3.x,
            bounds.min.x,
            bounds.max.x
        );

        vector3.y = Mathf.Clamp(
            vector3.y,
            bounds.min.y,
            bounds.max.y
        );

        vector3.z = Mathf.Clamp(
            vector3.z,
            bounds.min.z,
            bounds.max.z
        );

        return vector3;
    }
}