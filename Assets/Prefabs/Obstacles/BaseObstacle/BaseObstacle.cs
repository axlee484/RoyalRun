using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    // [SerializeField] private GameObject obstacle;
    private Rigidbody body;    // Update is called once per frame
    private float horizontalAcceleration = 0f;
    public float HorizontalAcceleration => horizontalAcceleration;
    public void SetHorizontalAcceleration(float value) => horizontalAcceleration = value;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        body.AddForce(Vector3.back * horizontalAcceleration, ForceMode.Acceleration);
    }


}
