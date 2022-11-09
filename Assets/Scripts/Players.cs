using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private string _player1Name, _player2Name;
    private int _player1Color, _player2Color; //black, blue, green
    public GameObject player1Tiles, player2Tiles;

    public GameObject[] p1SelectedColor, p2SelectedColor;

    void Start()
    {
        _player1Color = 0;
        _player2Color = 1;
    }

    public string Player1Name
    {
        get => _player1Name;
        set => _player1Name = value;
    }

    public string Player2Name
    {
        get => _player2Name;
        set => _player2Name = value;
    }

    public int Player1Color
    {
        get => _player1Color;
        set => _player1Color = value;
    }

    public int Player2Color
    {
        get => _player2Color;
        set => _player2Color = value;
    }

    public void Player1ChangeColor(int i)
    {
        if (_player2Color == i)
        {
            _player2Color = (_player2Color + 1) % 3;
        }
        for (int j = 0; j < 3; j++)
        {
            p2SelectedColor[j].SetActive(false);
        }
        p2SelectedColor[_player2Color].SetActive(true);
    }
    
    public void Player2ChangeColor(int i)
    {
        if (_player1Color == i)
        {
            _player1Color = (_player1Color + 1) % 3;
            for (int j = 0; j < 3; j++)
            {
                p1SelectedColor[j].SetActive(false);
            }
            p1SelectedColor[_player1Color].SetActive(true);
        }
    }
}
