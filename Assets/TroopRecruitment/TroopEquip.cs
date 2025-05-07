using Mono.Cecil;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TroopEquip : MonoBehaviour, IPointerClickHandler
{

    public string accepted;
    public Image unitIcon;

    [HideInInspector]
    public TroopStats current;

    public void Start()
    {

        unitIcon.enabled = false;

    }

    public bool canEquip(TroopStats unit)
    {

        if (string.IsNullOrEmpty(accepted) || accepted == "Any" || unit.type == accepted)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    public void setItem(TroopStats newArt)
    {

          current = newArt;
          unitIcon.sprite = newArt.icon;
          unitIcon.enabled = true; 

    }

    public void Clear()
    {

        current = null;
        unitIcon.sprite = null;
        unitIcon.enabled = false;

    }

    public void returnTo()
    {

        if (current != null)
        {

            if (TroopStorage.tps.addTroops(current))
            {

                Clear();

            }

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {

            if (current != null)
            {

                TroopStorage.tps.showStats(current);

            }

        }

    }

}
