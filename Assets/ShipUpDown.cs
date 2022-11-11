using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUpDown : MonoBehaviour
{
    public GameObject ship1, ship2;
    public float speed1, speed2;
    public float height1, height2;

    private bool a, b;

    private float x, y;

    public void Start()
    {
        a = true;
        b = true;
        x = ship1.transform.localPosition.y;
        y = ship2.transform.localPosition.y;
    }

    public void Update()
    {
        if (ship1.transform.localPosition.y <= (x + height1) && a)
        {
            ship1.transform.localPosition = new Vector3(ship1.transform.localPosition.x, ship1.transform.localPosition.y + speed1, ship1.transform.localPosition.z);
            if (ship1.transform.localPosition.y >= (x + height1)) a = false;
        }
        else if (ship1.transform.localPosition.y >= (x - height1) && !a)
        {
            ship1.transform.localPosition = new Vector3(ship1.transform.localPosition.x, ship1.transform.localPosition.y - speed1, ship1.transform.localPosition.z);
            if (ship1.transform.localPosition.y <= (x - height1)) a = true;
        }
        if (ship2.transform.localPosition.y <= (y + height2) && b)
        {
            ship2.transform.localPosition = new Vector3(ship2.transform.localPosition.x,ship2.transform.localPosition.y + speed2, ship2.transform.localPosition.z);
            if (ship2.transform.localPosition.y >= (y + height2)) b = false;
        }
        else if (ship2.transform.localPosition.y >= (y - height2) && !b)
        {
            ship2.transform.localPosition = new Vector3(ship2.transform.localPosition.x,ship2.transform.localPosition.y - speed2, ship2.transform.localPosition.z);
            if (ship2.transform.localPosition.y <= (y - height2)) b = true;
        }
    }
}
