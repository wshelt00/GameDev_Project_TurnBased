using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement controls;
    public Tilemap groundTileMap;
    public Tilemap treeTileMap;
    public GameObject panel;

    public int maxMove = 10; 
    private int currentMove;

    private bool turn = false; 

    private bool holdMove; //Determines if player hold down wasd keys to move

    public Slider MoveBar;
    public Button EndTurn;
    public TurnManager TurnManager;

    private IEnumerator coroutine;
    private Rigidbody2D rb;
    public Button yes;
    public Button no;

    private void Awake()
    {
        controls = new PlayerMovement();
        EndTurn.onClick.AddListener(endCheck);
        rb = GetComponent<Rigidbody2D>();
        panel.SetActive(false);
        yes.onClick.AddListener(confirmEnd);
        no.onClick.AddListener(cancelEnd);

        controls.Main.Movement.performed += HoldMoving; //For holddown movement
        controls.Main.Movement.canceled += NotHoldMoving;

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    
    void Start()
    {

        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>()); //Invoke movement when wasd keys are tap

    }

    public bool GetHoldMove() //get method for holdMove
    {
        return holdMove;
    }
    private void HoldMoving(InputAction.CallbackContext context) //If holding down wasd keys, holdMove is true
    {
        holdMove = true;
    }

    private void NotHoldMoving(InputAction.CallbackContext context) //If not holding down keys, holdMove is false
    {
        holdMove = false;
    }

    private void Move(Vector2 direction) //This method executes when player taps on movement keys. It checks to see if all conditions are met for movement
    {

        if (turn == true && currentMove > 0 && canMove(direction)) 
        {
            
            rb.MovePosition((Vector2) rb.position + direction);
            currentMove--; 
            UpdateUI();
            
            coroutine = Wait(direction); //This variable will store the wait method
            StartCoroutine(coroutine); //Will executes the wait method

        } 

    }

    private IEnumerator Wait(Vector2 direction) //This method will start a coroutine, which delay the move function call by 0.3f seconds, if the wasd keys are hold down
    {
        yield return new WaitForSeconds(0.3f); //The amount of time of delay

        if(GetHoldMove()) //If the player is still holding down the wasd keys after 0.3f seconds, the Move() method will execute to move the player 
        {

            Move(direction);

        } 
    }

    private bool canMove(Vector2 direction) //This part checks to see if player is near a object. If near a object, return false and if not, return true
    {

        Vector3Int gridPos = groundTileMap.WorldToCell(transform.position + (Vector3)direction);

        if (!groundTileMap.HasTile(gridPos) || treeTileMap.HasTile(gridPos))
        {
            return false;
        } else
        {
            return true;
        }

    }

    public void newTurn() 
    {
        turn = true; 
        currentMove = maxMove; 
        UpdateUI(); 
    }

    private void endTurn()
    {

      /*  if (turn == false) this is here is we want to add any AI players later
        {                    taking out this comment will cause the script to think it's the AI's turn
            return;          since that has not been added yet, this will cause the player to be unable to move so don't remove this
        } */

      //  turn = false;

        TurnManager.startTurn();

    } 

    private void UpdateUI() 
    {
        if (MoveBar != null) 
        {
            MoveBar.value = currentMove; 
        }
    }

    private void endCheck() 
    {

        if (currentMove > 0)
        {

            panel.SetActive(true);

        }
        else
        {

            confirmEnd();

        }

    }

    private void confirmEnd()
    {

        panel.SetActive(false);
        turn = false;
        endTurn();

    }

    private void cancelEnd()
    {

        panel.SetActive(false);

    }

}
