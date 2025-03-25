using UnityEngine;

public class GolfBallDetector : MonoBehaviour
{
    public GameObject golfBall, winningScreen;  // �١���쿷����Ҩе�Ǩ�Ѻ
    public CameraFollower cameraFollower;  // ���������Ѻ������§�Ѻ CameraFollower script

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == golfBall)  // ����١��������������
        {
            Destroy(other.gameObject);
            cameraFollower.OnBallInHole();  // �駡��ͧ��� "�١����ŧ����"
            winningScreen.SetActive(true);
        }
    }
}
