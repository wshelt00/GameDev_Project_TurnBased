using System.Collections.Generic;
using System.Runtime.Serialization;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.CollectionHelper;
using static UnityEngine.UI.CanvasScaler;

public class TroopStats : MonoBehaviour
{

    public static TroopStats Instance;

    public string unitName;
    public string type;
    public Sprite icon;

    public int attack;
    public int defense;

    public int HP = 50;
    public int MaxHP = 50;
    public int zoneIndx;
    public int gridPos = 0;

    public bool ranged;
    public bool inmelee;
    public GameObject unitObj;
    public GameObject hlght;


    public bool mounted;
    public bool playerUnit;
    public bool engagement;
    public bool defeated;
    public bool dummy = false;

    public Vector3 ogPos;

    public void clearstats()
    {

        unitName = "";
        attack = 0;
        defense = 0;
        icon = null;
        type = "";
        ranged = false;
        inmelee = false;
        defeated = true;

        Image img = GetComponent<Image>();
        if(img != null)
        {

            img.sprite = null;

        }

        if(hlght != null)
        {

            hlght.SetActive(false);

        }

    }

    public void setstats(TroopStats data)
    {

        unitName = data.unitName;
        attack = data.attack + InventoryManager.inv.globalATK;
        defense = data.defense + InventoryManager.inv.globalDef;
        HP = data.HP;
        type = data.type;
        ranged = data.ranged;
        ogPos = data.ogPos;
        icon = data.icon;
        inmelee = false;
        defeated = false;

        Image img = GetComponent<Image>();

        img.sprite = icon;

        if(img == null)
        {

            img.sprite = icon;

        }

    }

}

