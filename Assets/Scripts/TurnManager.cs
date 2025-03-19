using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{ 

    public PlayerController player;

    void Start() // figure out what to do with this, runs only once when the script is initialized
    {

        startTurn();
        
    } 

    public void startTurn()
    {

        player.newTurn();

    }
   
}
