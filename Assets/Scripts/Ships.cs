using System;
using System.Collections;
using System.Collections.Generic;
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
            blackShips[index].transform.localPosition = new Vector3(posx, height, posy);
            if (rotate) blackShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else blackShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (color == 1)
        {
            blueShips[index].transform.localPosition = new Vector3(posx, height, posy);
            if (rotate) blueShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else blueShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (color == 2)
        {
            greenShips[index].transform.localPosition = new Vector3(posx, height, posy);
            if (rotate) greenShips[index].transform.localEulerAngles = new Vector3(0, 90, 0);
            else greenShips[index].transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    public float[] GetCoord(GameObject[] tiles)
    {
        float posx = 0, posy = 0;
        foreach (GameObject tile in tiles)
        {
            posx += (int)tile.transform.localPosition.x;
            posy += (int)tile.transform.localPosition.z;
        }
        posx = posx / tiles.Length;
        posy = posy / tiles.Length;
        return new float[] {posx, posy};
    }

    public void RemoveShip()
    {
        foreach (GameObject ship in blackShips)
        {
            ship.transform.localPosition = new Vector3(1000, ship.transform.localPosition.y, 1000);
        }
        foreach (GameObject ship in blueShips)
        {
            ship.transform.localPosition = new Vector3(1000, ship.transform.localPosition.y, 1000);
        }
        foreach (GameObject ship in greenShips)
        {
            ship.transform.localPosition = new Vector3(1000, ship.transform.localPosition.y, 1000);
        }
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

    public void showShip(int color, int index)
    {
        if (color == 0)
        {
            blackShips[index].SetActive(true);
        }
        else if (color == 1)
        {
            blueShips[index].SetActive(true);
        }
        else if (color == 2)
        {
            greenShips[index].SetActive(true);
        }
    }
    
    public void hideShip(int color, int index)
    {
        if (color == 0)
        {
            blackShips[index].SetActive(false);
        }
        else if (color == 1)
        {
            blueShips[index].SetActive(false);
        }
        else if (color == 2)
        {
            greenShips[index].SetActive(false);
        }
    }
    
}
