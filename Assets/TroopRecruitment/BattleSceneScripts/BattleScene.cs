using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        TroopStorage.tps.equipStats();
        SceneManager.LoadScene("BattleScene");

    }

}
