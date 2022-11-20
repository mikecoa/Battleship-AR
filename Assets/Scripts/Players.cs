using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Players : MonoBehaviour
{
    private string _player1Name;
    private int _player1Color, _player2Color; //black, blue, green

    public Map map;
    public Ships ship;
    public GameObject mainBackground;
    public Camera arcam, cam;
    public GameObject mainPanel, prepPanel;

    void Start()
    {
        _player1Name = "";
        _player1Color = 0;
        _player2Color = 1;
        cam.clearFlags = CameraClearFlags.Skybox;
    }

    public string Player1Name
    {
        get => _player1Name;
        set => _player1Name = value;
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
    }

    public void Play()
    {
        ship.RemoveShip();
        map.Play();
        mainPanel.SetActive(false);
        prepPanel.SetActive(true);
        arcam.gameObject.SetActive(true);
        mainBackground.SetActive(false);
    }
    public void HideP1Ships()
    {
        ship.HideColorShip(_player1Color);
    }

    public void HideP2Ships()
    {
        ship.HideColorShip(_player2Color);
    }
}
