using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentManager : MonoBehaviour
{

    public static DeploymentManager dm;
    public BattleTurnManager btm;

    public List<TroopStats> playerUnits;
    public List<TroopStats> enemyUnits;

    public void DeployUnits()
    {

        loadEnemys();
        loadUnits();

        if (btm != null)
        {

            btm.playerUnits = playerUnits;
            btm.enemyUnits = enemyUnits;

            btm.StartCoroutine(btm.BeginBattle());

        }

    }

    public void loadUnits()
    {

        var equipUnits = TroopStorage.tps.equStats;

        for (int i = 0; i < playerUnits.Count; i++)
        {

            if (i < equipUnits.Count && equipUnits[i].icon != null)
            {

                playerUnits[i].unitName = equipUnits[i].unitName;
                playerUnits[i].attack = equipUnits[i].attack;
                playerUnits[i].defense = equipUnits[i].defense;
                playerUnits[i].type = equipUnits[i].type;
                playerUnits[i].ranged = equipUnits[i].ranged;
                playerUnits[i].HP = equipUnits[i].HP;

                playerUnits[i].icon = equipUnits[i].icon;
                playerUnits[i].unitObj.GetComponent<Image>().sprite = equipUnits[i].icon;
                playerUnits[i].unitObj.GetComponent<Image>().enabled = true;

            }

        }

    }

    public void loadEnemys()
    {

        var enemy = EnemyDeployment.ed.enemies;

        for (int i = 0; i < enemyUnits.Count; i++)
        {

            if (i < enemy.Count && enemy[i].icon != null)
            {

                enemyUnits[i].unitName = enemy[i].unitName;
                enemyUnits[i].attack = enemy[i].attack;
                enemyUnits[i].defense = enemy[i].defense;
                enemyUnits[i].type = enemy[i].type;
                enemyUnits[i].ranged = enemy[i].ranged;
                enemyUnits[i].HP = enemy[i].HP;

                enemyUnits[i].icon = enemy[i].icon;
                enemyUnits[i].unitObj.GetComponent<Image>().sprite = enemy[i].icon;
                enemyUnits[i].unitObj.GetComponent<Image>().enabled = true;

            }

        }

    }

}
