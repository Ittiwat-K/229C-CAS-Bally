using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallyMain : MonoBehaviour
{
    public int healths = 3;
    public  Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject loseScreenUI;
    public GameObject winningScreenUI;

    // Update is called once per frame
    void Update()
    {

        if (healths == 0)
        {
            loseScreenUI.SetActive(true);

            foreach (Image img in hearts)
            {
                img.gameObject.SetActive(false);
            }

            return;
        }


        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < healths; i++)
        {
            hearts[i].sprite = fullHeart;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            winningScreenUI.SetActive(true);

            foreach (Image img in hearts)
            {
                img.gameObject.SetActive(false);
            }

            return;
        }
    }
    
}
