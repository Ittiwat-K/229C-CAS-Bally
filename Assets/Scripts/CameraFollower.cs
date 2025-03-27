using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; 
    [SerializeField] float cameraSpeed; 
    [SerializeField] Vector3 offset; 
    private bool isBallInHole = false; 
    [SerializeField] float rotationSpeed; 
    private float currentHorizontalAngle; 
    private float currentVerticalAngle; 
    private Rigidbody golfBallRb; 

    void Start()
    {
        if (target != null)
        {
            golfBallRb = target.GetComponent<Rigidbody>();
        }
    }

    void LateUpdate()
    {
        if (target != null && !isBallInHole)
        {
                if (golfBallRb != null && golfBallRb.linearVelocity.magnitude < 0.01f)
                {
                    float horizontalInput = Input.GetAxis("Horizontal"); 
                    currentHorizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;

                    float verticalInput = Input.GetAxis("Vertical"); 
                    currentVerticalAngle += verticalInput * rotationSpeed * Time.deltaTime;
                }

                Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0); 
                Vector3 rotatedOffset = rotation * offset; 

                transform.position = target.position + rotatedOffset;

                transform.LookAt(target);
        }
    }

    public void OnBallInHole()
    {
        isBallInHole = true;  
    }
}
