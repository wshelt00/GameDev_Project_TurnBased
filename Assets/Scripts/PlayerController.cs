using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement controls;
    public Tilemap groundTileMap;
    public Tilemap treeTileMap;
    public GameObject panel;

    public int maxMove = 10; 
    private int currentMove;

    private bool turn = false; 

    public Slider MoveBar;
    public Button EndTurn;
    public TurnManager TurnManager;
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

        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());

    }

    private void Move(Vector2 direction)
    {

        if (turn == true && currentMove > 0 && canMove(direction)) 
        {
            
            rb.MovePosition((Vector2) rb.position + direction);
            currentMove--; 
            UpdateUI();

        } 

    }

    private bool canMove(Vector2 direction)
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
