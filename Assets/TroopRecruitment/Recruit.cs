using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : MonoBehaviour 
{

    public TroopStats[] unitSale;
    public int[] prices;
    List<TroopStats> soldUnits;
    List<int> soldPrices;
    private TroopStats selected;
    private TroopStats previous;
    private int selectedPrice;
    public int total;
    int j;

    public Text priceText;
    public Button conf;

    private void Start()
    {

        conf.interactable = false;
        conf.onClick.AddListener(unitPurchase);

    }

    public void unitSelection(int i)
    {

        if (i < 0 || i >= unitSale.Length)
        {
            return;
        }

        if (selected == unitSale[i])
        {

            clearItem();
            return;

        }

        if (previous != null)
        {

            previous.GetComponent<Image>().color = Color.white;

        }

        selected = unitSale[i];
        selectedPrice = prices[i];
        priceText.text = selectedPrice.ToString() + " Gold";

        selected.GetComponent<Image>().color = Color.yellow;
        previous = selected;

        conf.interactable = true;

    }

    public void unitPurchase()
    {

        if (selected != null && ResourceManager.resource.gold >= selectedPrice)
        {

            ResourceManager.resource.gold -= selectedPrice;
            ResourceManager.resource.goldUpdate();

            TroopStorage.tps.addTroops(selected);

            soldUnits = new List<TroopStats>(unitSale);
            soldPrices = new List<int>(prices);

            j = System.Array.IndexOf(unitSale, selected);

            if (j != -1)
            {

                soldUnits.RemoveAt(j);
                soldPrices.RemoveAt(j);

            }

            unitSale = soldUnits.ToArray();
            prices = soldPrices.ToArray();

            selected.gameObject.SetActive(false);

            clearItem();

        }

    }

    public void clearItem()
    {

        if (selected != null)
        {
            selected.GetComponent<Image>().color = Color.white;
        }

        selected = null;
        selectedPrice = 0;
        priceText.text = " ";
        conf.interactable = false;

    }

}
