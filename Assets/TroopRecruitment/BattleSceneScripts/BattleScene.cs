using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{

    public DeploymentManager dm;
    public static BattleScene sc;
    public TurnManager tm;
    public GameObject deployUI;
    public AudioSource audio;
    //public AudioClip btlmusic;
    public AudioSource ovrmusic;

    private int move = 0;
    private int threshold = 100;

    public List<TroopStats> enemyUnits;

    public Animator animator;
    public Animator UIanimator;
    public Button fight;
    public Button flee;
    public Button troops;
    public Button inv;

    public bool toggle = false;
    public bool toggleUI = false;

    private void Awake()
    {

        sc = this;

    }

    void Start()
    {

        fight.gameObject.SetActive(false);
        flee.gameObject.SetActive(false);
        deployUI.SetActive(false);
        deployUI.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        EnemyDeployment.ed.setEnemy(enemyUnits);

        if (deployUI != null)
        {

            fight.gameObject.SetActive(true);
            flee.gameObject.SetActive(true);

            troops.enabled = false;
            inv.enabled = false;

            toggleUI = !toggleUI;
            UIanimator.SetBool("IsEntered", toggleUI);

        }

    }

    public void BattleUI()
    {

        TroopStorage.tps.equipStats();

        UIanimator.SetBool("IsEntered", false);
        toggle = true;
        animator.SetBool("IsEntered", toggle);

        if (toggle == true && dm != null)
        {

            dm.DeployUnits();

        } 

        if(ovrmusic != null)
        {

            ovrmusic.Stop();

        }

        if(audio != null)
        {

            audio.loop = true;
            audio.Play();

        }

        tm.inBattle = true;

    }

    public void Cycle()
    {

        animator.SetBool("IsEntered", false);
        fight.gameObject.SetActive(false);
        flee.gameObject.SetActive(false);
        tm.inBattle = false;

        if(audio != null && audio.isPlaying)
        {

            audio.Stop();

        }

        if(ovrmusic != null)
        {

            ovrmusic.Play();

        }

    }

    public void UICycle()
    {

        UIanimator.SetBool("IsEntered", false);
        fight.gameObject.SetActive(false);
        flee.gameObject.SetActive(false);
        troops.enabled = true;
        inv.enabled = true;
        tm.inBattle = false;

        if (audio != null && audio.isPlaying)
        {

            audio.Stop();

        }

        if (ovrmusic != null)
        {

            ovrmusic.Play();

        }

    }

  /*  public void moveCount()
    {

        move++;
        int randomVal = Random.Range(0, 96);

        if (move + randomVal > threshold)
        {

            troops.enabled = false;
            inv.enabled = false;
            tm.inBattle = true;

            EnemyDeployment.ed.setEnemy(enemyUnits);
            TroopStorage.tps.equipStats();

            if (deployUI != null)
            {

                UIanimator.SetBool("IsEntered", true);

                fight.gameObject.SetActive(true);
                flee.gameObject.SetActive(true);

                move = 0;

            }

        }

    } */

}