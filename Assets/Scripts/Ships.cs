using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Ships : MonoBehaviour
{
    public GameObject[] blackShips, blueShips, greenShips;
    public GameObject ships;

    void Start()
    {
        ships.transform.position = new Vector3(-150, ships.transform.position.y, ships.transform.position.z);
    }

    public void PlaceShip(int color, int index, float posx, float posy)
    {
        float height;
        if (index < 2) height = -4;
        else if (index < 6) height = -2;
        else height = 0;
        
        if (color == 0)
        {
            blackShips[index].transform.position = new Vector3(posx, height, posy);
        }
        else if (color == 1)
        {
            blueShips[index].transform.position = new Vector3(posx, height, posy);
        }
        else if (color == 2)
        {
            greenShips[index].transform.position = new Vector3(posx, height, posy);
        }
    }

    public float[] GetCoord(GameObject[] tiles)
    {
        float posx = 0, posy = 0;
        foreach (GameObject tile in tiles)
        {
            posx += (int)tile.transform.position.x;
            posy += (int)tile.transform.position.z;
        }
        posx = posx / tiles.Length;
        posy = posy / tiles.Length;
        return new float[] {posx, posy};
    }
}
