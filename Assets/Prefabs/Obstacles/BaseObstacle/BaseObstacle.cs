using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    // [SerializeField] private GameObject obstacle;
    private Rigidbody body;    // Update is called once per frame
    private float appliedHorizontalForce = 0f;
    public float AppliedHorizontalForce => appliedHorizontalForce;
    public void ApplyHorizontalForce(float value) => appliedHorizontalForce = value;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        body.AddForce(Vector3.back * appliedHorizontalForce*Time.fixedDeltaTime);
    }


}
