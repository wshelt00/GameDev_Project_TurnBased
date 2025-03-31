using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.Compilation;

public class MerchantInventory : MonoBehaviour
{

    public Item[] itemSale;
    public int[] prices;
    List<Item> soldItems;
    List<int> soldPrices;
    private Item selected;
    private Item previous;
    private int selectedPrice;
    public int total;
    int j;

    public Text priceText;
    public Button conf;

    private void Start()
    {

        conf.interactable = false;
        conf.onClick.AddListener(itemPurchase);

    }

    public void itemSelection(int i)
    {

        if (i < 0 || i >= itemSale.Length)
        {
            return;
        }

        if (selected == itemSale[i])
        {

            clearItem();
            return;

        }

        if (previous != null)
        {

            previous.GetComponent<Image>().color = Color.white;

        }

        selected = itemSale[i];
        selectedPrice = prices[i];
        priceText.text = selectedPrice.ToString() + " Gold";

        selected.GetComponent<Image>().color = Color.yellow;
        previous = selected;

        conf.interactable = true;

    }

    public void itemPurchase()
    {

        if (selected != null && ResourceManager.resource.gold >= selectedPrice)
        {

            ResourceManager.resource.gold -= selectedPrice;
            ResourceManager.resource.goldUpdate();
            InventoryManager.inv.addItem(selected);

            soldItems = new List<Item>(itemSale);
            soldPrices = new List<int>(prices);

            j = System.Array.IndexOf(itemSale, selected);

            if (j != -1)
            {

                soldItems.RemoveAt(j);
                soldPrices.RemoveAt(j);

            }

            itemSale = soldItems.ToArray();
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
