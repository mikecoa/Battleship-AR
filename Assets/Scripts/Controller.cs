using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Map map;
    public Ships ships;
    private int currentSizeMode;//0: large, 1: medium, 2: small
    private int[] currentCursor2x2, currentCursor2x1, currentCursor1x1;
    void Start()
    {
        currentSizeMode = 0;
        currentCursor1x1 = new int[] {0};
        currentCursor2x1 = new int[] {0,1};
        currentCursor2x2 = new int[] {0,1,10,11};
    }

    public void Move(int s)
    {
        if (currentSizeMode == 0)
        {
            if (s == 0)
            {
                if (currentCursor2x2[1] > 9)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        currentCursor2x2[index] -= 10;
                    }
                }
            }
            if (s == 1)
            {
                if (currentCursor2x2[0]%10 != 0)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        currentCursor2x2[index] -= 1;
                    }
                }
            }
            if (s == 2)
            {
                if ((currentCursor2x2[1]+1)%10 != 0)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        currentCursor2x2[index] += 1;
                    }
                }
            }
            if (s == 3)
            {
                if (currentCursor2x2[3] < 90)
                {
                    for (int index = 0; index < 4; index++)
                    {
                        currentCursor2x2[index] += 10;
                    }
                }
            }
        }
        map.HighlightAreaP1(currentCursor2x2);
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
            ships.PlaceShip(0,0,ships.GetCoord(tiles)[0],
                ships.GetCoord(tiles)[1]);
        }
    }
}
