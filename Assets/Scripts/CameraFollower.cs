using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // ลูกกอล์ฟที่กล้องจะติดตาม
    [SerializeField] float cameraSpeed; // ความเร็วในการเคลื่อนที่ของกล้อง
    [SerializeField] Vector3 offset; // ระยะห่างระหว่างกล้องและลูกกอล์ฟ
    private bool isBallInHole = false; // ใช้เก็บสถานะว่าลูกกอล์ฟเข้าไปในหลุมแล้วหรือยัง
    [SerializeField] float rotationSpeed; // ความเร็วในการหมุนกล้อง
    private float currentHorizontalAngle; // มุมหมุนของกล้องในแนวนอน
    private float currentVerticalAngle; // มุมหมุนของกล้องในแนวตั้ง
    private Rigidbody golfBallRb; // Rigidbody ของลูกกอล์ฟ

    void Start()
    {
        if (target != null)
        {
            golfBallRb = target.GetComponent<Rigidbody>(); // เข้าถึง Rigidbody ของลูกกอล์ฟ
        }
    }

    void LateUpdate()
    {
        if (target != null && !isBallInHole) // ถ้าลูกกอล์ฟยังอยู่และยังไม่ลงหลุม
        {
                // ตรวจสอบว่าลูกกอล์ฟเคลื่อนที่ช้าเพียงพอ (velocity น้อยกว่า 0.1f)
                if (golfBallRb != null && golfBallRb.linearVelocity.magnitude < 0.01f)
                {
                    // การหมุนกล้องในแนวนอน (ซ้าย/ขวา) ด้วยปุ่ม A/D
                    float horizontalInput = Input.GetAxis("Horizontal"); // ตรวจจับการกดปุ่ม A/D หรือ ลูกศร
                    currentHorizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;

                    // การหมุนกล้องในแนวตั้ง (ขึ้น/ลง) ด้วยปุ่ม W/S
                    float verticalInput = Input.GetAxis("Vertical"); // ตรวจจับการกดปุ่ม W/S หรือ ลูกศร
                    currentVerticalAngle += verticalInput * rotationSpeed * Time.deltaTime;
                }

                // คำนวณตำแหน่งของกล้องใหม่ตามมุมหมุน
                Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0); // การหมุนทั้งในแนวตั้งและแนวนอน
                Vector3 rotatedOffset = rotation * offset; // ใช้มุมหมุนในการคำนวณตำแหน่งกล้อง

                // อัพเดทตำแหน่งของกล้อง
                transform.position = target.position + rotatedOffset;

                // กล้องจะหันไปมองที่ลูกกอล์ฟเสมอ
                transform.LookAt(target);
        }
    }

    public void OnBallInHole()
    {
        isBallInHole = true;  // ตั้งค่าสถานะว่า "ลูกกอล์ฟลงหลุมแล้ว"
    }
}
