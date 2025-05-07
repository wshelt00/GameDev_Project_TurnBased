using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class TroopStorage : MonoBehaviour
{

    public List<Slots> storedUnits = new List<Slots>();
    public List<TroopEquip> equUnit = new List<TroopEquip>();

    public List<TroopStats> equStats = new List<TroopStats>();

    public static TroopStorage tps;

    public bool toggle = false;
    public Button troopBut;
    public GameObject troopInv;
    public Animator animator;

    private TroopStats sts;
    public Text attack;
    public Text defense;
    public Text hp;
    public Text unitName;

    void Awake()
    {

        if (tps == null)
        {

            tps = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
   
            Destroy(gameObject);

        }

    }

    public void showStats(TroopStats unit)
    {

        attack.text = "Attack: " + unit.attack.ToString();
        defense.text = "Defense: " + unit.defense.ToString();
        hp.text = "HP: " + unit.HP.ToString();
        unitName.text = unit.unitName.ToString();

    }


    public bool addTroops(TroopStats troop)
    {

        for (int i = 0; i < storedUnits.Count; i++)
        {

            if (storedUnits[i].Empty())
            {

                storedUnits[i].setItem(troop);
                return true;

            }

        }

        return false;

    }

    public void equipUnits(TroopStats units, Slots slot)
    {

        for (int i = 0; i < equUnit.Count; i++)
        {

            if (equUnit[i].canEquip(units))
            {

                if (equUnit[i].current != null)
                {

                    TroopStorage.tps.addTroops(equUnit[i].current);

                }

                slot.slotClear();
                equUnit[i].setItem(units);
                equipStats();
                return;

            }

        }

    }

    public void equipStats()
    {

        equStats.Clear();

        for (int i = 0; i < equUnit.Count; i++)
        {

            equStats.Add(equUnit[i].current);
            
        }

    }

    public void copyList(List<TroopStats> units)
    {

        for(int i = 0; i < equUnit.Count; i++)
        {

            if (i < units.Count && units[i] != null)
            {

                equUnit[i].setItem(units[i]);

            } else
            {

                equUnit[i].Clear();

            }

        }

    }


    public void autoEquip(TroopStats troop, Slots slot)
    {

        for (int i = 0; i < equUnit.Count; i++)
        {

            if (equUnit[i].canEquip(troop) && equUnit[i].current == null)
            {

                equUnit[i].setItem(troop);
                slot.slotClear();
                equipStats();
                return;

            }

        }

    }

    public void toggleTroopInv()
    {

        if(InventoryManager.inv != null && InventoryManager.inv.invPanel.activeSelf) 
        {

            InventoryManager.inv.invPanel.SetActive(false);

        }

        toggle = !toggle;
        animator.SetBool("IsEntered", toggle);

    }



}