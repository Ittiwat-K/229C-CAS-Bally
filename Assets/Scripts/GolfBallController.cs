using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // �����ç㹡�õ�
    [SerializeField] float verticalStrength; // �����觢ͧ�١
    [SerializeField] float minDamping = 0.5f; // ��ҵ���ش�ͧ Linear Damping
    [SerializeField] float maxDamping = 2.0f; // ����٧�ش�ͧ Linear Damping
    [SerializeField] float maxHp = 100f;  // ����٧�ش�ͧ HP
    [SerializeField] float currentHp;  // ��� HP �Ѩ�غѹ
    [SerializeField] float dampingInterval; // ��ǧ��������㹡������ damping ����
    private Rigidbody rb;  // ����÷���红����� Rigidbody �ͧ�١����
    private float golfBallVelocity = 0.1f;  // �������Ƿ���١���쿵�ͧ��ӡ��ҡ�͹���е�������
    private int hitCount; // �ӹǹ㹡�õ�
    private Vector3 lastHitPosition;  // �纵��˹�����ش�ͧ�١���쿡�͹��
    private Collider ballCollider;  // ����÷���红����� Collider �ͧ�١����
    private PhysicsMaterial ballPhysicsMaterial;  // ����÷���红����� PhysicMaterial �ͧ�١����

    // ��ҧ�ԧ�֧ UIManager �������ʴ��Ţ�����
    public UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // �� Rigidbody �ͧ�١����
        ballCollider = GetComponent<Collider>();  // �� Collider �ͧ�١����
        ballPhysicsMaterial = ballCollider.sharedMaterial;  // �� PhysicMaterial �ͧ�١����
        currentHp = maxHp;  // ��駤��������鹢ͧ HP
        lastHitPosition = transform.position;  // ��駵��˹��������

        // ���¡�� InvokeRepeating �������� damping �ء� �Թҷշ���˹�
        InvokeRepeating("RandomDamping", 0f, dampingInterval);

        // ���¡��ѧ��ѹ�ҡ UIManager �����ѻവ UI �͹�������
        UpdateUI();
    }

    void Update()
    {
        // ��Ǩ�ͺ����١������ش���������� (�������ǵ�ӡ��Ҥ�ҷ���˹�)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {   
            // �Ǻ�����õ��١����
            if (Input.GetMouseButtonDown(0)) // ��ԡ�����������͵��Һ
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ��ȷҧ����١�ж١�� (���������ͧ)
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);  // �����ç㹷�ȷҧ����˹�
                lastHitPosition = transform.position; // �ѹ�֡���˹�����ش��͹��
                hitCount++;
            }

            if (Input.GetMouseButtonDown(1)) // ��ԡ����������͵���
            {
                Vector3 hitDirection = Camera.main.transform.forward;  // ��ȷҧ����١�ж١�� (���������ͧ)
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength;  // ������ȷҧ�ǵ����������١��
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);  // �����ç㹷�ȷҧ����˹�
                lastHitPosition = transform.position; // �ѹ�֡���˹�����ش��͹��
                hitCount++;
            }
        }
        
        // �ѻവ UI �ء��������� frame
        UpdateUI();
    }

    void RandomDamping()
    {
        // ������� linearDamping ����㹪�ǧ����˹�
        float randomDamping = Random.Range(minDamping, maxDamping);
        rb.linearDamping = randomDamping;
    }

    void UpdateUI()
    {
        // ���¡��ѧ��ѹ UpdateUI �ҡ UIManager �����ѻവ UI
        if (uiManager != null)
        {
            uiManager.UpdateUI(currentHp, hitCount, rb.linearDamping);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��Ǩ�ͺ����硢ͧ�ѵ�ط�誹��� "damage" ���� "heal"
        if (collision.gameObject.CompareTag("damage"))
        {
            // Ŵ HP ����ͪ��Ѻ�ѵ�ط������ "damage"
            currentHp -= 10f;  // Ŵ HP �������˹�

            // ���絵��˹��١���쿡�Ѻ价����˹�����ش��͹���ж١��
            transform.position = lastHitPosition;
            rb.linearVelocity = Vector3.zero;  // ��駤������Ǣͧ�١������ 0 ������ش�������͹���
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            // ���� HP ����ͪ��Ѻ�ѵ�ط������ "heal"
            currentHp += 10f;  // ���� HP �������˹�
            if (currentHp > maxHp)  // ��Ǩ�ͺ��� HP ����Թ����٧�ش
                currentHp = maxHp;
        }

        // ��Ǩ�ͺ HP �������ӡ����ٹ��
        if (currentHp <= 0f)
        {
            currentHp = 0f;
        }
    }
}
