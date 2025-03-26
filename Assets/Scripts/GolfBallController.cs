using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // ความแรงในการตี
    [SerializeField] float verticalStrength; // ความโด่งของลูก
    [SerializeField] float minDamping = 0.5f; // ค่าต่ำสุดของ Linear Damping
    [SerializeField] float maxDamping = 2.0f; // ค่าสูงสุดของ Linear Damping
    [SerializeField] float maxHp = 100f;  // ค่าสูงสุดของ HP
    [SerializeField] float currentHp;  // ค่า HP ปัจจุบัน
    [SerializeField] float dampingInterval; // ช่วงระยะเวลาในการสุ่ม damping ใหม่
    private Rigidbody rb;  // ตัวแปรที่เก็บข้อมูล Rigidbody ของลูกกอล์ฟ
    private float golfBallVelocity = 0.1f;  // ความเร็วที่ลูกกอล์ฟต้องต่ำกว่าก่อนที่จะตีใหม่ได้
    private int hitCount; // จำนวนในการตี
    private Vector3 lastHitPosition;  // เก็บตำแหน่งล่าสุดของลูกกอล์ฟก่อนตี
    private Collider ballCollider;  // ตัวแปรที่เก็บข้อมูล Collider ของลูกกอล์ฟ
    private PhysicsMaterial ballPhysicsMaterial;  // ตัวแปรที่เก็บข้อมูล PhysicMaterial ของลูกกอล์ฟ

    // อ้างอิงถึง UIManager ที่จะใช้แสดงผลข้อมูล
    public UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // เก็บ Rigidbody ของลูกกอล์ฟ
        ballCollider = GetComponent<Collider>();  // เก็บ Collider ของลูกกอล์ฟ
        ballPhysicsMaterial = ballCollider.sharedMaterial;  // เก็บ PhysicMaterial ของลูกกอล์ฟ
        currentHp = maxHp;  // ตั้งค่าเริ่มต้นของ HP
        lastHitPosition = transform.position;  // ตั้งตำแหน่งเริ่มต้น

        // เรียกใช้ InvokeRepeating เพื่อสุ่ม damping ทุกๆ วินาทีที่กำหนด
        InvokeRepeating("RandomDamping", 0f, dampingInterval);

        // เรียกใช้ฟังก์ชันจาก UIManager เพื่ออัปเดต UI ตอนเริ่มต้น
        UpdateUI();
    }

    void Update()
    {
        // ตรวจสอบว่าลูกกอล์ฟหยุดนิ่งหรือไม่ (ความเร็วต่ำกว่าค่าที่กำหนด)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {   
            // ควบคุมการตีลูกกอล์ฟ
            if (Input.GetMouseButtonDown(0)) // คลิกเมาส์ซ้ายเพื่อตีราบ
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ทิศทางที่ลูกจะถูกตี (ตามมุมกล้อง)
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);  // เพิ่มแรงในทิศทางที่กำหนด
                lastHitPosition = transform.position; // บันทึกตำแหน่งล่าสุดก่อนตี
                hitCount++;
            }

            if (Input.GetMouseButtonDown(1)) // คลิกเมาส์ขวาเพื่อตีโด่ง
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ทิศทางที่ลูกจะถูกตี (ตามมุมกล้อง)
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength;  // เพิ่มทิศทางแนวตั้งเพื่อให้ลูกโด่ง
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);  // เพิ่มแรงในทิศทางที่กำหนด
                lastHitPosition = transform.position; // บันทึกตำแหน่งล่าสุดก่อนตี
                hitCount++;
            }
        }
        
        // อัปเดต UI ทุกครั้งในแต่ละ frame
        UpdateUI();
    }

    void RandomDamping()
    {
        // สุ่มค่า linearDamping ใหม่ในช่วงที่กำหนด
        float randomDamping = Random.Range(minDamping, maxDamping);
        rb.linearDamping = randomDamping;
    }

    void UpdateUI()
    {
        // เรียกใช้ฟังก์ชัน UpdateUI จาก UIManager เพื่ออัปเดต UI
        if (uiManager != null)
        {
            uiManager.UpdateUI(currentHp, hitCount, rb.linearDamping);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ตรวจสอบว่าแท็กของวัตถุที่ชนคือ "damage" หรือ "heal"
        if (collision.gameObject.CompareTag("damage"))
        {
            // ลด HP เมื่อชนกับวัตถุที่มีแท็ก "damage"
            currentHp -= 10f;  // ลด HP ตามที่กำหนด

            // รีเซ็ตตำแหน่งลูกกอล์ฟกลับไปที่ตำแหน่งล่าสุดก่อนที่จะถูกตี
            transform.position = lastHitPosition;
            rb.linearVelocity = Vector3.zero;  // ตั้งความเร็วของลูกกอล์ฟเป็น 0 เพื่อหยุดการเคลื่อนที่
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            // เพิ่ม HP เมื่อชนกับวัตถุที่มีแท็ก "heal"
            currentHp += 10f;  // เพิ่ม HP ตามที่กำหนด
            if (currentHp > maxHp)  // ตรวจสอบว่า HP ไม่เกินค่าสูงสุด
                currentHp = maxHp;
        }

        // ตรวจสอบ HP ไม่ให้ต่ำกว่าศูนย์
        if (currentHp <= 0f)
        {
            currentHp = 0f;
        }
    }
}
