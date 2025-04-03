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

    public void itemSelection(int i)  //select an item from the list and update the UI
    {

        if (i < 0 || i >= itemSale.Length) // check if i is out of bounds
        {
            return;
        }

        if (selected == itemSale[i]) // check if same item has been selected again
        {

            clearItem();
            return;

        }

        if (previous != null) // reset previous selected items color
        {

            previous.GetComponent<Image>().color = Color.white;

        }

        selected = itemSale[i];  // set the selected variable to the user selected item and its price
        selectedPrice = prices[i];
        priceText.text = selectedPrice.ToString() + " Gold";

        selected.GetComponent<Image>().color = Color.yellow; 
        previous = selected;

        conf.interactable = true; 

    }

    public void itemPurchase() // purchase the item and remove it from the list
    {

        if (selected != null && ResourceManager.resource.gold >= selectedPrice) // check if an item has been selected and if the player has enough gold
        {

            ResourceManager.resource.gold -= selectedPrice;
            ResourceManager.resource.goldUpdate();

            InventoryManager.inv.addItem(selected); 

            soldItems = new List<Item>(itemSale); // see if you can't have the data structures used be the same type, ie all lists instead of arrays and lists
            soldPrices = new List<int>(prices);

            j = System.Array.IndexOf(itemSale, selected); // finds the index of the purchased item

            if (j != -1) // remove the purchased item and its associated price from the list
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
