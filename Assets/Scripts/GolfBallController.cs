using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // ความแรงในการตี
    [SerializeField] float verticalStrength; // ความโด่งของลูก
    [SerializeField] float minDamping = 0.5f; // ค่าต่ำสุดของ Linear Damping
    [SerializeField] float maxDamping = 2.0f; // ค่าสูงสุดของ Linear Damping
    [SerializeField] float maxHp = 100f;  // ค่าสูงสุดของ HP
    [SerializeField] float currentHp;  // ค่า HP ปัจจุบัน
    private Rigidbody rb;  // ตัวแปรที่เก็บข้อมูล Rigidbody ของลูกกอล์ฟ
    private float golfBallVelocity = 0.1f;  // ความเร็วที่ลูกกอล์ฟต้องต่ำกว่าก่อนที่จะตีใหม่ได้

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // เก็บ Rigidbody ของลูกกอล์ฟ
        currentHp = maxHp;  // ตั้งค่าเริ่มต้นของ HP
    }

    void Update()
    {
        // ตรวจสอบว่าลูกกอล์ฟหยุดนิ่งหรือไม่ (ความเร็วต่ำกว่าค่าที่กำหนด)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {
            float randomDamping = Random.Range(minDamping, maxDamping); // สุ่มค่าจาก minDamping ถึง maxDamping
            rb.linearDamping = randomDamping; // ตั้งค่า drag ให้กับ Rigidbody
            Debug.Log("Drag :" + randomDamping);

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

    void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่าแท็กของวัตถุที่ชนคือ "damage" หรือ "heal"
        if (collision.gameObject.CompareTag("damage"))
        {
            // ลด HP เมื่อชนกับวัตถุที่มีแท็ก "damage"
            currentHp -= 10f;  // ลด HP ตามที่กำหนด
            Debug.Log("HP :" + currentHp);
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            // เพิ่ม HP เมื่อชนกับวัตถุที่มีแท็ก "heal"
            currentHp += 10f;  // เพิ่ม HP ตามที่กำหนด
            if (currentHp > maxHp)  // ตรวจสอบว่า HP ไม่เกินค่าสูงสุด
                currentHp = maxHp;
            Debug.Log("HP :" + currentHp);
        }

        // ตรวจสอบ HP ไม่ให้ต่ำกว่าศูนย์
        if (currentHp <= 0f)
        {
            currentHp = 0f;
            Debug.Log("Game Over!");
        }
    }
}
