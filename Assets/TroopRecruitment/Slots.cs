using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slots : MonoBehaviour, IPointerClickHandler
{

    public Image icons;
    public TroopStats current;

    public void setItem(TroopStats selItem)
    {

        current = selItem;
        icons.sprite = selItem.icon;
        icons.enabled = true;
        icons.color = Color.white;

    }

    public bool Empty()
    {

        if (current == null)
        {

            return true;

        }
        else if (icons.sprite == null)
        {

            return true;

        }
        else
        {

            return false;

        }

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

        TroopStorage.tps.autoEquip(current, this);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(eventData.button == PointerEventData.InputButton.Right)
        {

            if(current != null)
            {

                TroopStorage.tps.showStats(current);

            }

        }

    }
}
