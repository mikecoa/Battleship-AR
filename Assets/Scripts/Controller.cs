using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Map map;
    public Ships ships;
    public Players players;
    public GameObject confirmButton, finishButton;
    public GameObject largeOutline, medOutline, smallOutline;
    public GameObject p1text, p2text;
    public TMP_Text popUpText;
    

    private int[] p1shipsLoc, p2shipsLoc;
    private int currentSizeMode;//0: large, 1: medium, 2: small
    private int[] currentCursor2x2, currentCursor2x1, currentCursor1x2, currentCursor1x1;
    private int cur2x2, cur2x1, cur1x1;
    private int rotate2x1; //0: 2x1, 1: 1x2
    private int curPlayer;
    void Start()
    {
        curPlayer = 0;
        currentSizeMode = 0;
        currentCursor1x1 = new int[] {0};
        currentCursor2x1 = new int[] {0,10};
        currentCursor1x2 = new int[] {0,1};
        currentCursor2x2 = new int[] {0,1,10,11};
        cur2x2 = 0;
        cur2x1 = 2;
        cur1x1 = 6;
        rotate2x1 = 0;
        p1shipsLoc = new int[100]; //-1: empty, 0-13: ships
        p2shipsLoc = new int[100];
        for (int i = 0; i < 100; i++)
        {
            p1shipsLoc[i] = -1;
            p2shipsLoc[i] = -1;
        }
    }

    public void Move(int s)
    {
        if (s == 0)//up
        {
            if (currentCursor2x2[1] > 9)
            {
                for (int index = 0; index < 4; index++)
                {
                    currentCursor2x2[index] -= 10;
                }
            }
            if (currentCursor2x1[1] > 9)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor2x1[index] -= 10;
                }
            }
            if (currentCursor1x2[1] > 9)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor1x2[index] -= 10;
                }
            }
            if (currentCursor1x1[0] > 9)
            {
                currentCursor1x1[0] -= 10; 
            }
        }
        if (s == 1)//left
        {
            if (currentCursor2x2[0] % 10 != 0)
            {
                for (int index = 0; index < 4; index++)
                {
                    currentCursor2x2[index] -= 1;
                }
            }
            if (currentCursor2x1[0] % 10 != 0)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor2x1[index] -= 1;
                }
            }
            if (currentCursor1x2[0] % 10 != 0)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor1x2[index] -= 1;
                }
            }
            if (currentCursor1x1[0] % 10 != 0)
            {
                currentCursor1x1[0] -= 1; 
            }
        }
        if (s == 2)//right
        {
            if ((currentCursor2x2[1] + 1) % 10 != 0)
            {
                for (int index = 0; index < 4; index++)
                {
                    currentCursor2x2[index] += 1;
                }
            }
            if ((currentCursor2x1[1] + 1) % 10 != 0)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor2x1[index] += 1;
                }
            }
            if ((currentCursor1x2[1] + 1) % 10 != 0)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor1x2[index] += 1;
                }
            }
            if ((currentCursor1x1[0] +1 ) % 10 != 0)
            {
                currentCursor1x1[0] += 1; 
            }
        }
        if (s == 3)//down
        {
            if (currentCursor2x2[3] < 90)
            {
                for (int index = 0; index < 4; index++)
                {
                    currentCursor2x2[index] += 10;
                }
            }
            if (currentCursor2x1[1] < 90)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor2x1[index] += 10;
                }
            }
            if (currentCursor1x2[1] < 90)
            {
                for (int index = 0; index < 2; index++)
                {
                    currentCursor1x2[index] += 10;
                }
            }
            if (currentCursor1x1[0] < 90)
            {
                currentCursor1x1[0] += 10; 
            }
        }
        if (currentSizeMode == 0) map.HighlightAreaP1(currentCursor2x2);
        else if (currentSizeMode == 1 && rotate2x1 == 0) map.HighlightAreaP1(currentCursor2x1);
        else if (currentSizeMode == 1 && rotate2x1 == 1) map.HighlightAreaP1(currentCursor1x2);
        else if (currentSizeMode == 2) map.HighlightAreaP1(currentCursor1x1);
    }

    public void AddShip()
    {
        GameObject[] tiles;
        if (currentSizeMode == 0 && cur2x2 < 2)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x2[0]],map.p1Map[currentCursor2x2[1]],
                map.p1Map[currentCursor2x2[2]],map.p1Map[currentCursor2x2[3]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor2x2[0]] == -1 && p1shipsLoc[currentCursor2x2[1]] == -1
               && p1shipsLoc[currentCursor2x2[2]] == -1 && p1shipsLoc[currentCursor2x2[3]] == -1)
            {
                p1shipsLoc[currentCursor2x2[0]] = cur2x2;
                p1shipsLoc[currentCursor2x2[1]] = cur2x2;
                p1shipsLoc[currentCursor2x2[2]] = cur2x2;
                p1shipsLoc[currentCursor2x2[3]] = cur2x2;
                ships.PlaceShip(players.Player1Color, cur2x2++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else if(curPlayer == 1 && p2shipsLoc[currentCursor2x2[0]] == -1 && p2shipsLoc[currentCursor2x2[1]] == -1
                    && p2shipsLoc[currentCursor2x2[2]] == -1 && p2shipsLoc[currentCursor2x2[3]] == -1)
            {
                p2shipsLoc[currentCursor2x2[0]] = cur2x2;
                p2shipsLoc[currentCursor2x2[1]] = cur2x2;
                p2shipsLoc[currentCursor2x2[2]] = cur2x2;
                p2shipsLoc[currentCursor2x2[3]] = cur2x2;
                ships.PlaceShip(players.Player2Color, cur2x2++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else showInvalid();
        }
        else if (currentSizeMode == 1 && rotate2x1 == 0 && cur2x1 < 6)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x1[0]],map.p1Map[currentCursor2x1[1]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor2x1[0]] == -1 && p1shipsLoc[currentCursor2x1[1]] == -1)
            {
                p1shipsLoc[currentCursor2x1[0]] = cur2x1;
                p1shipsLoc[currentCursor2x1[1]] = cur2x1;
                ships.PlaceShip(players.Player1Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else if (curPlayer == 1 && p2shipsLoc[currentCursor2x1[0]] == -1 && p2shipsLoc[currentCursor2x1[1]] == -1)
            {
                p2shipsLoc[currentCursor2x1[0]] = cur2x1;
                p2shipsLoc[currentCursor2x1[1]] = cur2x1;
                ships.PlaceShip(players.Player2Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else showInvalid();
        }
        else if (currentSizeMode == 1 && rotate2x1 == 1 && cur2x1 < 6)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x2[0]],map.p1Map[currentCursor1x2[1]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor1x2[0]] == -1 && p1shipsLoc[currentCursor1x2[1]] == -1)
            {
                p1shipsLoc[currentCursor1x2[0]] = cur2x1;
                p1shipsLoc[currentCursor1x2[1]] = cur2x1;
                ships.PlaceShip(players.Player1Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], true);
                showRemain();
            }
            else if (curPlayer == 1 && p2shipsLoc[currentCursor1x2[0]] == -1 && p2shipsLoc[currentCursor1x2[1]] == -1)
            {
                p2shipsLoc[currentCursor1x2[0]] = cur2x1;
                p2shipsLoc[currentCursor1x2[1]] = cur2x1;
                ships.PlaceShip(players.Player2Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], true);
                showRemain();
            }
            else showInvalid();
        }
        else if (currentSizeMode == 2 && cur1x1 < 14)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x1[0]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor1x1[0]] == -1)
            {
                p1shipsLoc[currentCursor1x1[0]] = cur1x1;
                ships.PlaceShip(players.Player1Color, cur1x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else if(curPlayer == 1 && p2shipsLoc[currentCursor1x1[0]] == -1)
            {
                p2shipsLoc[currentCursor1x1[0]] = cur1x1;
                ships.PlaceShip(players.Player2Color, cur1x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                showRemain();
            }
            else showInvalid();
        }
        if (cur1x1 == 14 && cur2x1 == 6 && cur2x2 == 2)
        {
            confirmButton.SetActive(false);
            finishButton.SetActive(true);
            showPlacedAllBoats();
        }
    }

    public void setSize(int s)
    {
        currentSizeMode = s;
        if (currentSizeMode == 0) map.HighlightAreaP1(currentCursor2x2);
        else if (currentSizeMode == 1 && rotate2x1 == 0) map.HighlightAreaP1(currentCursor2x1);
        else if (currentSizeMode == 1 && rotate2x1 == 1) map.HighlightAreaP1(currentCursor1x2);
        else if (currentSizeMode == 2) map.HighlightAreaP1(currentCursor1x1);
        showRemain();
    }

    public void Rotate()
    {
        if (rotate2x1 == 0)
        {
            rotate2x1 = 1;
            map.HighlightAreaP1(currentCursor1x2);
        }
        else if (rotate2x1 == 1)
        {
            rotate2x1 = 0;
            map.HighlightAreaP1(currentCursor2x1);
        }
    }

    public void ChangePlayer()
    {
        if (curPlayer == 0)
        {
            currentSizeMode = 0;
            currentCursor1x1 = new int[] {0};
            currentCursor2x1 = new int[] {0,10};
            currentCursor1x2 = new int[] {0,1};
            currentCursor2x2 = new int[] {0,1,10,11};
            cur2x2 = 0;
            cur2x1 = 2;
            cur1x1 = 6;
            rotate2x1 = 0;
            curPlayer = 1;
            players.HideP1Ships();
            confirmButton.SetActive(true);
            finishButton.SetActive(false);
            smallOutline.SetActive(false);
            medOutline.SetActive(false);
            largeOutline.SetActive(true);
            map.HighlightAreaP1(currentCursor2x2);
            p1text.SetActive(false);
            p2text.SetActive(true);
        }
    }

    public void showRemain()
    {
        if (currentSizeMode == 0)
        {
            popUpText.SetText((2 - cur2x2) + " of 2 Large ships left");
        }
        else if (currentSizeMode == 1)
        {
            popUpText.SetText((4 - (cur2x1 - 2)) + " of 4 Medium ships left");
        }
        else if (currentSizeMode == 2)
        {
            popUpText.SetText((8 - (cur1x1 - 6)) + " of 8 Small ships left");
        }
        popUpText.color = Color.blue;
        StartCoroutine(Countdown(1));
    }

    public void showInvalid()
    {
        popUpText.SetText("Space is occupied by another boat");
        popUpText.color = Color.red;
        StartCoroutine(Countdown(2));
    }

    public void showPlacedAllBoats()
    {
        popUpText.SetText("You have placed all your boats! Press finish.");
        popUpText.color = Color.green;
        StartCoroutine(Countdown(3));
    }
    
    private IEnumerator Countdown(int s)
    {
        popUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds (s);
        popUpText.gameObject.SetActive(false);
    }
}
