using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private string _player1Name, _player2Name;
    private int _player1Color, _player2Color; //black, blue, green
    public GameObject player1Tiles, player2Tiles;

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

}
