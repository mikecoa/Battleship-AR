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
    
    public void PlaceShip(int color, int index, float posx, float posy, bool rotate)
    {
        float height;
        if (index < 2) height = -4;
        else if (index < 6) height = -2;
        else height = 0;
        
        if (color == 0)
        {
            blackShips[index].transform.position = new Vector3(posx, height, posy);
            if (rotate) blackShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else blackShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (color == 1)
        {
            blueShips[index].transform.position = new Vector3(posx, height, posy);
            if (rotate) blueShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else blueShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (color == 2)
        {
            greenShips[index].transform.position = new Vector3(posx, height, posy);
            if (rotate) greenShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else greenShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
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

    public void RemoveShip()
    {
        ships.transform.position = new Vector3(-150, ships.transform.position.y, ships.transform.position.z);
    }

    public void HideColorShip(int color)
    {
        if (color == 0)
        {
            foreach (GameObject ship in blackShips)
            {
                ship.SetActive(false);
            }
        }
        else if (color == 1)
        {
            foreach (GameObject ship in blueShips)
            {
                ship.SetActive(false);
            }
        }
        else if (color == 2)
        {
            foreach (GameObject ship in greenShips)
            {
                ship.SetActive(false);
            }
        }
    }
}
