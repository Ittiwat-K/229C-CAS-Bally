using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text statsText;  // สำหรับแสดงค่า HP, Hit Count, Damping

    public void UpdateUI(float currentHp, int hitCount, float damping)
    {
        if (statsText != null)
        {
            // แสดงข้อความและตัวเลขในบรรทัดเดียวกัน
            statsText.text = "HP : " + currentHp.ToString("F0") + " | " +  // แสดง HP
                             "Hit Count : " + hitCount.ToString() + " | " +  // แสดง Hit Count
                             "Damping : " + damping.ToString("F1");  // แสดง Damping
        }
    }
}
