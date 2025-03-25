using UnityEngine;

public class GolfBallDetector : MonoBehaviour
{
    public GameObject golfBall, winningScreen;  // ลูกกอล์ฟที่เราจะตรวจจับ
    public CameraFollower cameraFollower;  // ตัวแปรสำหรับเชื่อมโยงกับ CameraFollower script

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == golfBall)  // ถ้าลูกกอล์ฟเข้ามาในหลุม
        {
            Destroy(other.gameObject);
            cameraFollower.OnBallInHole();  // แจ้งกล้องว่า "ลูกกอล์ฟลงหลุม"
            winningScreen.SetActive(true);
        }
    }
}
