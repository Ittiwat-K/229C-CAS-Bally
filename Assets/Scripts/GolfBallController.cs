using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  // �����ç㹡�õ�
    [SerializeField] float verticalStrength; // �����觢ͧ�١
    private Rigidbody rb;  // ����÷���红����� Rigidbody �ͧ�١����
    private float golfBallVelocity = 0.01f;  // �������Ƿ���١���쿵�ͧ��ӡ��ҡ�͹���е�������

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // �� Rigidbody �ͧ�١����
    }

    void Update()
    {
        // ��Ǩ�ͺ����١������ش���������� (�������ǵ�ӡ��Ҥ�ҷ���˹�)
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {
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
}
