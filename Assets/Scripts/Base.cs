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

    [SerializeField]
    private Tilemap tilemapCardCover;

    [Space(10)]
    [Header("PREFABS")]

    [SerializeField]
    private TileBase  tileCardCover;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------- //


    private int[] card1;

    private int[] card2;

    private List<int[]> path = new List<int[]>();


    // ------------------------------------------------------------------------------------------------------------------------------------------------------------- //


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

                        if (card1 == null)
                        {
                            card1 = new int[2] { i, j };

                            tilemapCardCover.SetTile(new Vector3Int(i, j, 0), tileCardCover);

                            Debug.Log("Card 1: " + tilemapItem.GetTile(new Vector3Int(card1[0], card1[1])).name + ": " + card1[0] + " - " + card1[1]);
                        }
                        else if (card2 == null)
                        {
                            if (i != card1[0] || j != card1[1])
                            {
                                card2 = new int[2] { i, j };

                                tilemapCardCover.SetTile(new Vector3Int(i, j, 0), tileCardCover);

                                Debug.Log("Card 2: " + tilemapItem.GetTile(new Vector3Int(card2[0], card2[1])).name + ": " + card2[0] + " - " + card2[1]);

                                StartCoroutine(ConnectCard());
                            }
                        }
                    }
                }
            }
        }
    }


    IEnumerator ConnectCard ()
    {
        if (tilemapItem.GetTile(new Vector3Int(card1[0], card1[1])).name == tilemapItem.GetTile(new Vector3Int(card2[0], card2[1])).name && CheckPath(card1[0], card1[1], card2[0], card2[1]))
        {
            yield return new WaitForSeconds(0.25f);

            tilemapCardCover.SetTile(new Vector3Int(card1[0], card1[1]), null);

            tilemapCardCover.SetTile(new Vector3Int(card2[0], card2[1]), null);

            tilemapCard.SetTile(new Vector3Int(card1[0], card1[1]), null);

            tilemapCard.SetTile(new Vector3Int(card2[0], card2[1]), null);

            tilemapItem.SetTile(new Vector3Int(card1[0], card1[1]), null);

            tilemapItem.SetTile(new Vector3Int(card2[0], card2[1]), null);

            Debug.Log("Connected");

            card1 = null;

            card2 = null;
        }
        else
        {
            yield return new WaitForSeconds(0.25f);

            tilemapCardCover.SetTile(new Vector3Int(card1[0], card1[1]), null);

            tilemapCardCover.SetTile(new Vector3Int(card2[0], card2[1]), null);

            card1 = null;

            card2 = null;
        }
    }

    private bool CheckPath (int x1, int y1, int x2, int y2)
    {
        int xL1;

        int xR1;

        int xL2;

        int xR2;

        // Check left - right limit 1

        for (xL1 = x1 - 1; xL1 > -1; xL1--)
        {
            if (tilemapItem.GetTile(new Vector3Int(xL1, y1)) != null)
            {
                xL1 ++;

                break;
            }          
        }

        for (xR1 = x1 + 1; xR1 < sizeX; xR1++)
        {
            if (tilemapItem.GetTile(new Vector3Int(xR1, y1)) != null)
            {
                xR1--;

                break;
            }
        }

        // Check left - right limit 2

        for (xL2 = x2 - 1; xL2 > -1; xL2--)
        {
            if (tilemapItem.GetTile(new Vector3Int(xL2, y2)) != null)
            {
                xL2++;

                break;
            }
        }

        for (xR2 = x2 + 1; xR2 < sizeX; xR2++)
        {
            if (tilemapItem.GetTile(new Vector3Int(xR2, y2)) != null)
            {
                xR2--;

                break;
            }
        }

        Debug.Log("Left - right limit" + xL1 + " " + xR1 + " " + xL2 + " " + xR2);

        //Check path C type

        for (int i = -1; i <= sizeX; i++)
        {
            if (i >= xL1 && i <= xR1 && i >= xL2 && i <= xR2)
            {
                if (y1 >= y2)
                {
                    int m = 0;

                    for (int j = y2 + 1; j <= y1 - 1; j++)
                    {
                        if (tilemapItem.GetTile(new Vector3Int(i, j)) != null)
                        {
                            m++;
                        }
                    }

                    if (m == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    int m = 0;

                    for (int j = y1 + 1; j <= y2 - 1; j++)
                    {
                        if (tilemapItem.GetTile(new Vector3Int(i, j)) != null)
                        {
                            m++;
                        }
                    }

                    if (m == 0)
                    {
                        return true;
                    }
                }
            }
        }


        int yU1;

        int yD1;

        int yU2;

        int yD2;


        //Check up - down limit 1

        for (yD1 = y1 - 1; yD1 > -1; yD1--)
        {
            if (tilemapItem.GetTile(new Vector3Int(x1, yD1)) != null)
            {
                yD1++;

                break;
            }
        }

        for (yU1 = y1 + 1; yU1 < sizeY; yU1++)
        {
            if (tilemapItem.GetTile(new Vector3Int(x1, yU1)) != null)
            {
                yU1--;

                break;
            }
        }

        //Check up - down limit 2

        for (yD2 = y2 - 1; yD2 > -1; yD2--)
        {
            if (tilemapItem.GetTile(new Vector3Int(x2, yD2)) != null)
            {
                yD2++;

                break;
            }
        }

        for (yU2 = y2 + 1; yU2 < sizeY; yU2++)
        {
            if (tilemapItem.GetTile(new Vector3Int(x2, yU2)) != null)
            {
                yU2--;

                break;
            }
        }

        for (int i = -1; i <= sizeY; i++)
        {
            if (i >= yD1 && i <= yU1 && i >= yD2 && i <= yU2)
            {
                if (x1 >= x2)
                {
                    int m = 0;

                    for (int j = x2 + 1; j <= x1 - 1; j++)
                    {
                        if (tilemapItem.GetTile(new Vector3Int(j, i)) != null)
                        {
                            m++;
                        }
                    }

                    if (m == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    int m = 0;

                    for (int j = x1 + 1; j <= x2 - 1; j++)
                    {
                        if (tilemapItem.GetTile(new Vector3Int(j, i)) != null)
                        {
                            m++;
                        }
                    }

                    if (m == 0)
                    {
                        return true;
                    }
                }
            }
        }

        Debug.Log("Up - down limit" + yD1 + " " + yU1 + " " + yD2 + " " + yU2);

        return false;
    }
}
