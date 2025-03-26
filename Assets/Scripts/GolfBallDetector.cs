using UnityEngine;

public class GolfBallDetector : MonoBehaviour
{
    public GameObject golfBall;
    public CameraFollower cameraFollower;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == golfBall)  // ถ้าลูกกอล์ฟเข้ามาในหลุม
        {
            Destroy(other.gameObject);
            cameraFollower.OnBallInHole();  // บอกกล้องว่า "ลูกกอล์ฟลงหลุม"
        }
    }
}
