using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] Rigidbody body;
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] Bounds boundingBox;
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
        var xDirection = Math.Sign(movement.x);
        var newPosition = body.position + horizontalSpeed * Time.fixedDeltaTime * xDirection * Vector3.right;
        newPosition = ClampVector3(newPosition, boundingBox);
        body.MovePosition(newPosition);
    }
}
