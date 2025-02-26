using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    private PlayerMovement controls;
    [SerializeField]
    private Tilemap groundTileMap;
    [SerializeField]
    private Tilemap treeTileMap;

    private void Awake()
    {
        controls = new PlayerMovement();
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

        if (canMove(direction))
        {
            transform.position += (Vector3)direction;
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

}
