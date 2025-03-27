using UnityEngine;
using TMPro;

public class AirMeter : MonoBehaviour
{
    public TMP_Text statsText;  // ����Ѻ�ʴ���� Damping

    public void UpdateUI(float damping)
    {
        if (statsText != null)
        {
            // �ʴ���ͤ�����е���Ţ㹺�÷Ѵ���ǡѹ
            statsText.text = "Air Resist : " + damping.ToString("F1");  // �ʴ� Damping
        }
    }
}
