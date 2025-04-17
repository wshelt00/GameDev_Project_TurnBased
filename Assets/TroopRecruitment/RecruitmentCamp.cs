using UnityEngine;
using UnityEngine.UI;

public class RecruitmentCamp : MonoBehaviour 
{

    private bool inRange = false;
    private bool toggle = false;
    public GameObject recruitUI;
    public Button cancel;

    void Start()
    {

        cancel.onClick.AddListener(closeRecruit);
        recruitUI.SetActive(false);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {

            inRange = true;
            recruitUI.SetActive(true);

        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            inRange = false;
            recruitUI.SetActive(false);

        }

    }

    private void closeRecruit()
    {

        recruitUI.SetActive(false);

    }

}
