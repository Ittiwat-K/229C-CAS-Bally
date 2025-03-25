using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject loseScreen;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            loseScreen.SetActive(true);
        }
    }

}
