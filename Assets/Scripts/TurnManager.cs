using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{ 

    public PlayerController player;

    void Start() 
    {

        startTurn();
        
    } 

    public void startTurn()
    {

        player.newTurn();

    }
   
}
