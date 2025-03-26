using UnityEngine;

public class GolfBallDetector : MonoBehaviour
{
    public GameObject golfBall;
    public CameraFollower cameraFollower;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == golfBall)  // ����١��������������
        {
            Destroy(other.gameObject);
            cameraFollower.OnBallInHole();  // �͡���ͧ��� "�١����ŧ����"
        }
    }
}
