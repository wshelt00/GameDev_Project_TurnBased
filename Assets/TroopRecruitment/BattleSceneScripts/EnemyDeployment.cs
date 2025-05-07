using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeployment : MonoBehaviour
{
    
    public List<TroopStats> enemies = new List<TroopStats>();
    public static EnemyDeployment ed;


    void Awake()
    {
        
        if(ed == null)
        {

            ed = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {

            Destroy(gameObject);

        }

    } 

    public void setEnemy(List<TroopStats> units)
    {

        enemies = new List<TroopStats>(units);

    }

}
