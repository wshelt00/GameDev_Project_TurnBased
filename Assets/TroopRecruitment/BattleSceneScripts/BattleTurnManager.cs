using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using NUnit.Framework;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleTurnManager : MonoBehaviour 
{

    public bool playerTurn = true;
    private Button RetreatButton;
    public Button troops;
    public Button inv;

    public float moveSpeed = 300f;
    private TroopStats clicked;
    private TroopStats playerUnit;
    private TroopStats lastHghlt;
    private TroopStats setStats;

    public BattleScene bs;
    public TroopStorage tps;
    public TurnManager tm;
    public DeploymentManager dm;

    public List<TroopStats> playerUnits;
    public List<TroopStats> enemyUnits;
    public List<TroopStats> activeUnits;

    public Transform[] playerSpots;
    public Transform[] enemySpots;

    public PlayerController player;

    private bool forClick;

    public IEnumerator BeginBattle()
    {

        for (int i = 0; i < playerUnits.Count; i++) // used to apply stat cahnges from equipped items
        {

            TroopStats unit = playerUnits[i];

            if (unit.gameObject.activeSelf == true)
            {

                unit.setstats(unit);

            }

        }

        for (int i = 0; i < playerUnits.Count; i++) //sets the stats for inactive image objects, then they are reactivated with the new unit's stats
        {

            TroopStats unit = playerUnits[i];

            if (unit.gameObject.activeSelf == false)
            {

                unit.setstats(unit);
                unit.gameObject.SetActive(true);

            }

        }

        for (int i = 0; i < enemyUnits.Count; i++) //sets the stats for inactive enemy image objects and when the battle UI opens again will respawn the enemy units that are in the enenmyUnits list in BattleScene.cs
        {

            TroopStats unit = enemyUnits[i];

            if (unit.gameObject.activeSelf == false)
            {

                unit.setstats(unit);
                unit.gameObject.SetActive(true);

            }

        }

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < playerUnits.Count; i++)
        {

            if (playerUnits[i] != null)
            {

                playerUnits[i].ogPos = playerUnits[i].unitObj.transform.position;

            }

        }

        for (int i = 0; i < enemyUnits.Count; i++)
        {

            if (enemyUnits[i] != null)
            {

                enemyUnits[i].ogPos = enemyUnits[i].unitObj.transform.position;

            }

        }

        tm.inBattle = true;

        StartCoroutine(TurnHandler());

    }

    IEnumerator TurnHandler()
    {

        while (playerUnits.Count > 0 && enemyUnits.Count > 0)
        {

            for (int i = 0; i < playerUnits.Count; i++)
            {

                if (enemyUnits.All(e => !e.gameObject.activeSelf || e.HP <= 0)) //if all enemy units are dead this will run
                {

                    break;

                }

                
                yield return PlayerATK(playerUnits[i], enemyUnits);

            }

            for (int i = 0; i < enemyUnits.Count; i++)
            {

                if (playerUnits.All(e => !e.gameObject.activeSelf || e.HP <= 0)) //if all player units are dead this will run
                {
                    

                    break;

                }

                yield return UntAct(enemyUnits[i], playerUnits);

            }

            if (enemyUnits.All(e => !e.gameObject.activeSelf || e.HP <= 0)) //if all enemy units are dead this will run
            {
                player.tag = "Winning";
                StopAllCoroutines();
                tm.inBattle = false;
                bs.Cycle();
                troops.enabled = true;
                inv.enabled = true;
                TroopStorage.tps.copyList(savedActive(playerUnits));

            }

        }

    }

    IEnumerator UntAct(TroopStats atk, List<TroopStats> enemy)
    {

        TroopStats trg = enemy[0];

        if (atk.ranged == true && atk.inmelee == false)
        {

            yield return Attack(atk, trg);

        }
        else
        {

            yield return moveTo(atk, trg);

        }

        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator PlayerATK(TroopStats atk, List<TroopStats> enemy)
    {

        clicked = null;
        forClick = true;
        playerUnit = atk;

        if (lastHghlt != null && lastHghlt.hlght != null)
        {

            lastHghlt.hlght.SetActive(false);

        }

        if (atk.hlght != null)
        {

            atk.hlght.SetActive(true);

        }

        lastHghlt = atk;

        while (clicked == null)
        {

            yield return null;

        }

        if (atk.ranged == true && atk.inmelee == false)
        {

            yield return Attack(atk, clicked);

        }
        else
        {

            yield return moveTo(atk, clicked);

        }

        yield return new WaitForSeconds(0.5f);

    }

    public void enemyClick(TroopStats enemy)
    {

        if (forClick == true && playerUnit != null)
        {

            clicked = enemy;
            forClick = false;

        }

    }

    IEnumerator moveTo(TroopStats attacker, TroopStats target)
    {

        if (attacker.defeated == true || target.defeated == true)
        {

            yield break;

        }

        int zoneIndex;
        Transform atkSpot;
        Transform trgSpot;

        if (playerUnits.Contains(attacker))
        {

            zoneIndex = playerUnits.IndexOf(attacker);

            atkSpot = playerSpots[zoneIndex];
            trgSpot = enemySpots[zoneIndex];

        }
        else
        {

            zoneIndex = enemyUnits.IndexOf(attacker);

            atkSpot = enemySpots[zoneIndex];
            trgSpot = playerSpots[zoneIndex];

        }

        GameObject atkObj = attacker.unitObj;
        GameObject trgObj = target.unitObj;

        Coroutine atkMove = StartCoroutine(Move(atkObj, atkSpot.position)); //moves the attacker to the center at the same time as the target
        Coroutine trgMove = StartCoroutine(Move(trgObj, trgSpot.position)); //moves the target to the center at the same time as the attacker

        yield return atkMove;
        yield return trgMove; 

        while (attacker.HP > 0 && target.HP > 0)
        {

            int damageTrg = attacker.attack - target.defense;

            if (damageTrg < 0)
            {

                damageTrg = 0;

            }

            target.HP -= damageTrg;

            if (target.HP <= 0)
            {
                break;
            }

            int damageAtk = target.attack - attacker.defense;

            if (damageAtk < 0)
            {

                damageAtk = 0;

            }

            attacker.HP -= damageAtk;

            if (attacker.ranged == false && target.ranged == true)
            {

                target.inmelee = true;

            }

            yield return new WaitForSeconds(0.5f);

        }

        if (attacker.HP <= 0)
        {

            Coroutine atkMoveBack = StartCoroutine(Move(atkObj, attacker.ogPos)); // both of these move the layer and enemy unit back at the same time
            Coroutine trgMoveBack = StartCoroutine(Move(trgObj, target.ogPos));   // if either of these aren't here, the units will spawn in the center during the next battle

            yield return atkMoveBack;
            yield return trgMoveBack;

            attacker.clearstats();
            attacker.unitObj.SetActive(false);

        }

        if (target.HP <= 0)
        {

            Coroutine atkMoveBack = StartCoroutine(Move(atkObj, attacker.ogPos)); // both of these move the layer and enemy unit back at the same time
            Coroutine trgMoveBack = StartCoroutine(Move(trgObj, target.ogPos));   // if either of these aren't here, the units will spawn in the center during the next battle

            yield return atkMoveBack;
            yield return trgMoveBack;

            target.clearstats();
            target.unitObj.SetActive(false);

        }

    }


    IEnumerator Move(GameObject unit, Vector3 dest)
    {

        while (Vector3.Distance(unit.transform.position, dest) > 0.1f)
        {

            unit.transform.position = Vector3.MoveTowards(unit.transform.position, dest, moveSpeed * Time.deltaTime);
            yield return null;

        }

    }

    IEnumerator Attack(TroopStats att, TroopStats targ)
    {

        if(att.dummy == true || targ.dummy == true)
        {

            yield break;

        }

        int damage = att.attack - targ.defense;

        if (damage < 0)
        {

            damage = 0;

        }

        targ.HP -= damage;

        if (targ.HP <= 0)
        {

            targ.clearstats();
            targ.unitObj.SetActive(false);

        }
        else
        {

            if (att.ranged == false && targ.ranged == true)
            {

                targ.inmelee = true;

            }

        }

        yield return null;

    }

    public void Retreat()
    {

        StopAllCoroutines();
        bs.Cycle();
        troops.enabled = true;
        inv.enabled = true;
        tm.inBattle = false;
        TroopStorage.tps.copyList(savedActive(playerUnits));

    }

    public List<TroopStats> savedActive(List<TroopStats> unit)
    {

        activeUnits.Clear();

        for (int i = 0; i < unit.Count; i++)
        {

            TroopStats troop = unit[i];

            if (troop.gameObject.activeSelf == true && troop.unitName != null )
            {

                activeUnits.Add(troop);

            }

        }

        return activeUnits;

    }

}