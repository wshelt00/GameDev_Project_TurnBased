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

    public int globalATK;
    public int globalDef;

    public void Awake()
    {
        
        if(inv == null)
        {

            inv = this;
            DontDestroyOnLoad(gameObject);

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

        if(TroopStorage.tps != null && TroopStorage.tps.troopInv.activeSelf) //here
        {

            TroopStorage.tps.animator.SetBool("IsEntered", false);
            TroopStorage.tps.toggle = false;

        }


        if(invPanel != null)
        {

            invPanel.SetActive(!invPanel.activeSelf);

        }

    }

    public bool addItem(Item art) 
    {

        for(int i = 0; i < invSlots.Count; i++)
        {

            if(invSlots[i].Empty())
            {

                invSlots[i].setItem(art);
                return true;

            }

        }

        return false;

    }

    public void equipItem(Item artfct, Inventory slot) 
    {

        for(int i = 0; i < equSlots.Count; i++)
        {

            if (equSlots[i].canEquip(artfct))
            {

                if (equSlots[i].current != null)
                {

                    InventoryManager.inv.addItem(equSlots[i].current);

                }
                
                slot.slotClear();
                equSlots[i].setItem(artfct);
                return;

            }

        }

    }
    
    public void autoEquip(Item relic, Inventory slot)
    {

        for(int i = 0;i < equSlots.Count; i++)
        {

            if (equSlots[i].canEquip(relic))
            {

                if (equSlots[i].current != null)
                {

                    InventoryManager.inv.addItem(equSlots[i].current);

                }

                equSlots[i].setItem(relic);
                slot.slotClear();
                return;

            }

        }

    }

}
