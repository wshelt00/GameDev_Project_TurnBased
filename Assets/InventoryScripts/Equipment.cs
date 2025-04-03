using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{

    public string accepted;
    public Image artIcon;

    [HideInInspector]
    public Item current;

    public void Start()
    {
        
        artIcon.enabled = false;

    }

    public bool canEquip(Item artifact) 
    {

        if(artifact.arti == accepted)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public void setItem(Item newArt)
    {

        current = newArt;
        artIcon.sprite = newArt.icon;
        artIcon.enabled = true;

    }

    public void Clear()
    {

        current = null;
        artIcon.sprite = null;
        artIcon.enabled = false;

    }

    public void returnTo()
    {

        if (current != null)
        {

            if (InventoryManager.inv.addItem(current))
            {

                Clear();

            }

        }

    }

}
