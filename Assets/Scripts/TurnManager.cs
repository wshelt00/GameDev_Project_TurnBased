using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{ 

    public PlayerController player;
    public bool inBattle = false;

    void Start() 
    {

        startTurn();
        StartCoroutine(Healing());
        
    } 

    public void startTurn()
    {

        player.newTurn();

    }

    IEnumerator Healing()
    {

        while(true)
        {

            if(inBattle == false)
            {

                HealEquipUnits();
                HealInvUnits();

            }

            yield return new WaitForSeconds(8f);

        }

    }

    public void HealEquipUnits()
    {

       foreach(TroopEquip equip in TroopStorage.tps.equUnit)
       {

          if(equip.current != null)
          {

             TroopStats unit = equip.current;
                    
             if(unit.HP < unit.MaxHP)
             {

                unit.HP += 5;

                if(unit.HP > unit.MaxHP)
                {

                   unit.HP = unit.MaxHP;

                }

             }

          }

       }

    }

    public void HealInvUnits()
    {

       foreach(Slots slot in TroopStorage.tps.storedUnits)
       {

          if (slot.current != null)
          {

             TroopStats unit = slot.current;

             if (unit.HP < unit.MaxHP)
             {

                 unit.HP += 5;

                 if (unit.HP > unit.MaxHP)
                 {

                     unit.HP = unit.MaxHP;

                 }

             }

          }

       }

    }
   
}
