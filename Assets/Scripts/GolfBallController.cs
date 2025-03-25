using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // �����ç㹡�õ�
    [SerializeField] float verticalStrength; // �����觢ͧ�١
    [SerializeField] float minDamping = 0.5f; // ��ҵ���ش�ͧ Linear Damping
    [SerializeField] float maxDamping = 2.0f; // ����٧�ش�ͧ Linear Damping
    [SerializeField] float maxHp = 100f;  // ����٧�ش�ͧ HP
    [SerializeField] float currentHp;  // ��� HP �Ѩ�غѹ
    private Rigidbody rb;  // ����÷���红����� Rigidbody �ͧ�١����
    private float golfBallVelocity = 0.1f;  // �������Ƿ���١���쿵�ͧ��ӡ��ҡ�͹���е�������

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // �� Rigidbody �ͧ�١����
        currentHp = maxHp;  // ��駤��������鹢ͧ HP
    }

    void Update()
    {
        // ��Ǩ�ͺ����١������ش���������� (�������ǵ�ӡ��Ҥ�ҷ���˹�)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {
            float randomDamping = Random.Range(minDamping, maxDamping); // ������Ҩҡ minDamping �֧ maxDamping
            rb.linearDamping = randomDamping; // ��駤�� drag ���Ѻ Rigidbody
            Debug.Log("Drag :" + randomDamping);

            // �Ǻ�����õ��١����
            if (Input.GetMouseButtonDown(0)) // ��ԡ�������������������
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ��ȷҧ����١�ж١�� (���������ͧ)
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);  // �����ç㹷�ȷҧ����˹�
            }

            if (Input.GetMouseButtonDown(1)) // ��ԡ��������������������
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ��ȷҧ����١�ж١�� (���������ͧ)
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength;  // ������ȷҧ�ǵ����������١��
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);  // �����ç㹷�ȷҧ����˹�
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��Ǩ�ͺ����硢ͧ�ѵ�ط�誹��� "damage" ���� "heal"
        if (collision.gameObject.CompareTag("damage"))
        {
            // Ŵ HP ����ͪ��Ѻ�ѵ�ط������ "damage"
            currentHp -= 10f;  // Ŵ HP �������˹�
            Debug.Log("HP :" + currentHp);
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            // ���� HP ����ͪ��Ѻ�ѵ�ط������ "heal"
            currentHp += 10f;  // ���� HP �������˹�
            if (currentHp > maxHp)  // ��Ǩ�ͺ��� HP ����Թ����٧�ش
                currentHp = maxHp;
            Debug.Log("HP :" + currentHp);
        }

        // ��Ǩ�ͺ HP �������ӡ����ٹ��
        if (currentHp <= 0f)
        {
            currentHp = 0f;
            Debug.Log("Game Over!");
        }
    }
}
