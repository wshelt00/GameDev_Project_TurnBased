using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public Image icons;
    private Item current;

    public void setItem(Item selItem)
    {

        current = selItem;
        icons.sprite = selItem.icon;
        icons.enabled = true;
        icons.color = Color.white;

    }

    public bool Empty()
    {

        return current == null || icons.sprite == null;

    }

    public void slotClear()
    {

        current = null;
        icons.sprite = null;
        icons.enabled = false;

    } 

    public void onClick()
    {

        if (current == null)
        {

            return;

        }

        InventoryManager.inv.autoEquip(current, this);

    }


}
