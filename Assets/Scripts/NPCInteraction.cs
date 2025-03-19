using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{

    public Button yes;
    public Button no;
    public Button accept;
    public Button cancel;
    public GameObject panel;
    public GameObject trade;
    private bool inRange = false;
    
    void Start()
    {

        panel.SetActive(false);
        trade.SetActive(false);
        yes.onClick.AddListener(tradingPanel);
        no.onClick.AddListener(closePanel);
        accept.onClick.AddListener(closeTrade);
        cancel.onClick.AddListener(closeTrade);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("Activating panel!");
            inRange = true;
            panel.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            inRange = false;
            panel.SetActive(false);
            trade.SetActive(false);

        }

    }

    private void tradingPanel()
    {

        panel.SetActive(false);
        trade.SetActive(true);

    }

    private void closePanel()
    {

        panel.SetActive(false);

    }

    private void closeTrade()
    {
        trade.SetActive(false);
    }


}
