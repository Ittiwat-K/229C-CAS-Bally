using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<BallyMain>().healths--;
        }
    }
}
