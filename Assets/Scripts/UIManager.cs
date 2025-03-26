using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text statsText;  // ����Ѻ�ʴ���� HP, Hit Count, Damping

    public void UpdateUI(float currentHp, int hitCount, float damping)
    {
        if (statsText != null)
        {
            // �ʴ���ͤ�����е���Ţ㹺�÷Ѵ���ǡѹ
            statsText.text = "HP : " + currentHp.ToString("F0") + " | " +  // �ʴ� HP
                             "Hit Count : " + hitCount.ToString() + " | " +  // �ʴ� Hit Count
                             "Damping : " + damping.ToString("F1");  // �ʴ� Damping
        }
    }
}
