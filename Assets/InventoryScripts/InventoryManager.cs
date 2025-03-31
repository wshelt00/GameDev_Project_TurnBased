using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager inv;

    public List<Inventory> invSlots = new List<Inventory>();
    public List<Equipment> equSlots = new List<Equipment>();

    public GameObject button;
    public GameObject invPanel;

    public void Awake()
    {
        
        if(inv == null)
        {

            inv = this;

        } else
        {

            Destroy(gameObject);

        }

        if(invPanel != null)
        {

            invPanel.SetActive(false);

        }


    }

    public void togglePanel()
    {

        if(invPanel != null)
        {

            invPanel.SetActive(!invPanel.activeSelf);

        }

    }

    public bool addItem(Item art) 
    {

        foreach (var space in invSlots)
        {

            if (space.Empty()) 
            {
                
                space.setItem(art);
                return true;

            }

        }

        return false;

    }

    public void equipItem(Item artfct, Inventory slot)
    {

        foreach (var space in equSlots)
        {

            if(space.canEquip(artfct))
            {

                if(space.current != null)
                {

                    InventoryManager.inv.addItem(space.current);

                }
                
                slot.slotClear();
                space.setItem(artfct);
                return;

            }

        }

    }
    
    public void autoEquip(Item relic, Inventory slot)
    {

        foreach (var space in equSlots)
        {

            if(space.canEquip(relic))
            {

                if(space.current != null)
                {

                    InventoryManager.inv.addItem(space.current);

                }

                space.setItem(relic);
                slot.slotClear();
                return;

            }

        }

    }

}
