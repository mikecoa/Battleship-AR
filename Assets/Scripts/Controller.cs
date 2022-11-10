using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Map map;
    public Ships ships;
    public Players players;
    private int currentSizeMode;//0: large, 1: medium, 2: small
    private int[] currentCursor2x2, currentCursor2x1, currentCursor1x2, currentCursor1x1;
    private int cur2x2, cur2x1, cur1x1;
    private int rotate2x1; //0: 2x1, 1: 1x2
    void Start()
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
        if (currentSizeMode == 0)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x2[0]],map.p1Map[currentCursor2x2[1]],
                map.p1Map[currentCursor2x2[2]],map.p1Map[currentCursor2x2[3]]
            };
            ships.PlaceShip(players.Player1Color,cur2x2++,ships.GetCoord(tiles)[0],
                ships.GetCoord(tiles)[1], false);
            if (cur2x2 > 1) cur2x2 = 0;
        }
        else if (currentSizeMode == 1 && rotate2x1 == 0)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x1[0]],map.p1Map[currentCursor2x1[1]]
            };
            ships.PlaceShip(players.Player1Color,cur2x1++,ships.GetCoord(tiles)[0],
                ships.GetCoord(tiles)[1], false);
            if (cur2x1 > 5) cur2x1 = 2;
        }
        else if (currentSizeMode == 1 && rotate2x1 == 1)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x2[0]],map.p1Map[currentCursor1x2[1]]
            };
            ships.PlaceShip(players.Player1Color,cur2x1++,ships.GetCoord(tiles)[0],
                ships.GetCoord(tiles)[1], true);
            if (cur2x1 > 5) cur2x1 = 2;
        }
        else if (currentSizeMode == 2)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x1[0]]
            };
            ships.PlaceShip(players.Player1Color,cur1x1++,ships.GetCoord(tiles)[0],
                ships.GetCoord(tiles)[1], false);
            if (cur2x1 > 13) cur1x1 = 6;
        }
    }

    public void setSize(int s)
    {
        currentSizeMode = s;
        if (currentSizeMode == 0) map.HighlightAreaP1(currentCursor2x2);
        else if (currentSizeMode == 1 && rotate2x1 == 0) map.HighlightAreaP1(currentCursor2x1);
        else if (currentSizeMode == 1 && rotate2x1 == 1) map.HighlightAreaP1(currentCursor1x2);
        else if (currentSizeMode == 2) map.HighlightAreaP1(currentCursor1x1);
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
}
