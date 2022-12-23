using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Base : MonoBehaviour
{
    [Space(1)]
    [Header("PROPERTIES")]

    [SerializeField]
    private int sizeX;

    [SerializeField]
    private int sizeY;


    [Space(10)]
    [Header("LOCAL OBJECT")]

    [SerializeField]
    private Tilemap tilemapCard;

    [SerializeField]
    private Tilemap tilemapItem;

    [Space(10)]
    [Header("PREFABS")]

    [SerializeField]
    private TileBase  tileBaseEmpty;

    private void OnEnable ()
    {
        
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (Vector3.Distance(new Vector3(tilemapItem.CellToWorld(new Vector3Int(i, j, 0)).x + 0.395f, tilemapItem.CellToWorld(new Vector3Int(i, j, 0)).y + 0.395f, 0), Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)) <= 10.005f)
                    {
                        //Debug.Log("H: " + i + ", V: " + j + " ~ Distance touch point: " + Vector3.Distance(new Vector3(tilemapItem.CellToWorld(new Vector3Int(i, j, 0)).x + 0.395f, tilemapItem.CellToWorld(new Vector3Int(i, j, 0)).y + 0.395f, 0), Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)));

                        tilemapItem.SetTile(new Vector3Int(i, j, 0), null);

                        tilemapCard.SetTile(new Vector3Int(i, j, 0), null);
                    }
                }
            }
        }
    }
}
