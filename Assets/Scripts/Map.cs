using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] p1Map, p2Map;
    

    public void HighlightAreaP1(int[] tiles)
    {
        for (int j = 0; j < 100; j++)
        {
            p1Map[j].SetActive(false);
        }
        for (int j = 0; j < tiles.Length; j++)
        {
            p1Map[tiles[j]].SetActive(true);
        }
    }

    public void Play()
    {
        HighlightAreaP1(new int[] {0,1,10,11});
    }
}
