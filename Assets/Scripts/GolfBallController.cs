using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // ความแรงในการตี
    [SerializeField] float verticalStrength; // ความโด่งของลูก
    private Rigidbody rb;  // ตัวแปรที่เก็บข้อมูล Rigidbody ของลูกกอล์ฟ
    private float golfBallVelocity = 0.01f;  // ความเร็วที่ลูกกอล์ฟต้องต่ำกว่าก่อนที่จะตีใหม่ได้

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // เก็บ Rigidbody ของลูกกอล์ฟ
    }

    void Update()
    {
        // ตรวจสอบว่าลูกกอล์ฟหยุดนิ่งหรือไม่ (ความเร็วต่ำกว่าค่าที่กำหนด)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {
            // ควบคุมการตีลูกกอล์ฟ
            if (Input.GetMouseButtonDown(0)) // คลิกเมาส์ซ้ายเพื่อเริ่มตี
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ทิศทางที่ลูกจะถูกตี (ตามมุมกล้อง)
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);  // เพิ่มแรงในทิศทางที่กำหนด
            }

            if (Input.GetMouseButtonDown(1)) // คลิกเมาส์ขวาเพื่อเริ่มตีโด่ง
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ทิศทางที่ลูกจะถูกตี (ตามมุมกล้อง)
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength;  // เพิ่มทิศทางแนวตั้งเพื่อให้ลูกโด่ง
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);  // เพิ่มแรงในทิศทางที่กำหนด
            }
        }
    }
}
