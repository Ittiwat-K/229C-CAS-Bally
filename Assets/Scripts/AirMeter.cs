using UnityEngine;
using TMPro;

public class AirMeter : MonoBehaviour
{
    public TMP_Text statsText;  // สำหรับแสดงค่า Damping

    public void UpdateUI(float damping)
    {
        if (statsText != null)
        {
            // แสดงข้อความและตัวเลขในบรรทัดเดียวกัน
            statsText.text = "Air Resist : " + damping.ToString("F1");  // แสดง Damping
        }
    }
}
