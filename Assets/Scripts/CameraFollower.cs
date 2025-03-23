using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // �١���쿷����ͧ�еԴ���
    [SerializeField] float cameraSpeed; // ��������㹡������͹���ͧ���ͧ
    [SerializeField] Vector3 offset; // ������ҧ�����ҧ���ͧ����١����
    private bool isBallInHole = false; // ����ʶҹ�����١����������������������ѧ
    [SerializeField] float rotationSpeed; // ��������㹡����ع���ͧ
    private float currentHorizontalAngle; // �����ع�ͧ���ͧ��ǹ͹
    private float currentVerticalAngle; // �����ع�ͧ���ͧ��ǵ��
    private Rigidbody golfBallRb; // Rigidbody �ͧ�١����

    void Start()
    {
        if (target != null)
        {
            golfBallRb = target.GetComponent<Rigidbody>(); // ��Ҷ֧ Rigidbody �ͧ�١����
        }
    }

    void LateUpdate()
    {
        if (target != null && !isBallInHole) // ����١�����ѧ��������ѧ���ŧ����
        {
                // ��Ǩ�ͺ����١��������͹�������§�� (velocity ���¡��� 0.1f)
                if (golfBallRb != null && golfBallRb.linearVelocity.magnitude < 0.01f)
                {
                    // �����ع���ͧ��ǹ͹ (����/���) ���»��� A/D
                    float horizontalInput = Input.GetAxis("Horizontal"); // ��Ǩ�Ѻ��á����� A/D ���� �١��
                    currentHorizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;

                    // �����ع���ͧ��ǵ�� (���/ŧ) ���»��� W/S
                    float verticalInput = Input.GetAxis("Vertical"); // ��Ǩ�Ѻ��á����� W/S ���� �١��
                    currentVerticalAngle += verticalInput * rotationSpeed * Time.deltaTime;
                }

                // �ӹǳ���˹觢ͧ���ͧ�����������ع
                Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0); // �����ع�����ǵ������ǹ͹
                Vector3 rotatedOffset = rotation * offset; // �������ع㹡�äӹǳ���˹觡��ͧ

                // �Ѿഷ���˹觢ͧ���ͧ
                transform.position = target.position + rotatedOffset;

                // ���ͧ���ѹ��ͧ����١��������
                transform.LookAt(target);
        }
    }

    public void OnBallInHole()
    {
        isBallInHole = true;  // ��駤��ʶҹ���� "�١����ŧ��������"
    }
}
