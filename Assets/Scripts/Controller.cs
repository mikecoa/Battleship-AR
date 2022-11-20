using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Controller : MonoBehaviour
{
    public Map map;
    public Ships ships;
    public Players players;
    public GameObject buttonGroup, finishButton;
    public GameObject largeOutline, medOutline, smallOutline;
    public TMP_Text popUpText, popUpP1AttackText, hitText, shipInd1P1, shipInd1CPU, shipInd2P1, shipInd2CPU, endText, bigText, medText, smallText;
    public GameObject prepPanel, waitPrepPanel, attackPanel, waitAttackPanel, endPanel;
    public GameObject blackShips, blueShips, greenShips;
    public GameObject attackButton;
    public ParticleSystem particleSystem;
    public AudioSource hit, miss;

    private int[] p1shipsLoc, p2shipsLoc;
    private int currentSizeMode;//0: large, 1: medium, 2: small
    private int[] currentCursor2x2, currentCursor2x1, currentCursor1x2, currentCursor1x1;
    private int cur2x2, cur2x1, cur1x1;
    private int rotate2x1; //0: 2x1, 1: 1x2
    private int curPlayer;
    private int cursor, enemycursor;
    private int playerShips, CPUShips;
    private int CPUHitStreak;
    private List<int> CPUcheck = new List<int>();
    void Start()
    {
        
        playerShips = 14;
        CPUShips = 14;
        cursor = 0;
        enemycursor = 0;
        curPlayer = 0;
        currentSizeMode = 0;
        currentCursor1x1 = new int[] {0};
        currentCursor2x1 = new int[] {0,10};
        currentCursor1x2 = new int[] {0,1};
        currentCursor2x2 = new int[] {0,1,10,11};
        cur2x2 = 0;
        cur2x1 = 2;
        cur1x1 = 6;
        rotate2x1 = 0;
        p1shipsLoc = new int[100]; //-1: empty, 0-13: ships
        p2shipsLoc = new int[100];
        for (int i = 0; i < 100; i++)
        {
            p1shipsLoc[i] = -1;
            p2shipsLoc[i] = -1;
        }
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
            if (currentCursor2x1[0] > 9)
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
        if (currentSizeMode == 0 && cur2x2 < 2)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x2[0]],map.p1Map[currentCursor2x2[1]],
                map.p1Map[currentCursor2x2[2]],map.p1Map[currentCursor2x2[3]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor2x2[0]] == -1 && p1shipsLoc[currentCursor2x2[1]] == -1
               && p1shipsLoc[currentCursor2x2[2]] == -1 && p1shipsLoc[currentCursor2x2[3]] == -1)
            {
                p1shipsLoc[currentCursor2x2[0]] = cur2x2;
                p1shipsLoc[currentCursor2x2[1]] = cur2x2;
                p1shipsLoc[currentCursor2x2[2]] = cur2x2;
                p1shipsLoc[currentCursor2x2[3]] = cur2x2;
                ships.PlaceShip(players.Player1Color, cur2x2++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                //showRemain();
                bigText.SetText((2 - (cur2x2))+"/2");
            }
            else if(curPlayer == 1 && p2shipsLoc[currentCursor2x2[0]] == -1 && p2shipsLoc[currentCursor2x2[1]] == -1
                    && p2shipsLoc[currentCursor2x2[2]] == -1 && p2shipsLoc[currentCursor2x2[3]] == -1)
            {
                p2shipsLoc[currentCursor2x2[0]] = cur2x2;
                p2shipsLoc[currentCursor2x2[1]] = cur2x2;
                p2shipsLoc[currentCursor2x2[2]] = cur2x2;
                p2shipsLoc[currentCursor2x2[3]] = cur2x2;
                ships.PlaceShip(players.Player2Color, cur2x2++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
            }
            else showInvalid();
        }
        else if (currentSizeMode == 1 && rotate2x1 == 0 && cur2x1 < 6)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor2x1[0]],map.p1Map[currentCursor2x1[1]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor2x1[0]] == -1 && p1shipsLoc[currentCursor2x1[1]] == -1)
            {
                p1shipsLoc[currentCursor2x1[0]] = cur2x1;
                p1shipsLoc[currentCursor2x1[1]] = cur2x1;
                ships.PlaceShip(players.Player1Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                //showRemain();
                medText.SetText((6 - (cur2x1))+"/4");
            }
            else if (curPlayer == 1 && p2shipsLoc[currentCursor2x1[0]] == -1 && p2shipsLoc[currentCursor2x1[1]] == -1)
            {
                p2shipsLoc[currentCursor2x1[0]] = cur2x1;
                p2shipsLoc[currentCursor2x1[1]] = cur2x1;
                ships.PlaceShip(players.Player2Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
            }
            else showInvalid();
        }
        else if (currentSizeMode == 1 && rotate2x1 == 1 && cur2x1 < 6)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x2[0]],map.p1Map[currentCursor1x2[1]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor1x2[0]] == -1 && p1shipsLoc[currentCursor1x2[1]] == -1)
            {
                p1shipsLoc[currentCursor1x2[0]] = cur2x1;
                p1shipsLoc[currentCursor1x2[1]] = cur2x1;
                ships.PlaceShip(players.Player1Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], true);
                //showRemain();
                medText.SetText((6 - (cur2x1))+"/4");
            }
            else if (curPlayer == 1 && p2shipsLoc[currentCursor1x2[0]] == -1 && p2shipsLoc[currentCursor1x2[1]] == -1)
            {
                p2shipsLoc[currentCursor1x2[0]] = cur2x1;
                p2shipsLoc[currentCursor1x2[1]] = cur2x1;
                ships.PlaceShip(players.Player2Color, cur2x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], true);
            }
            else showInvalid();
        }
        else if (currentSizeMode == 2 && cur1x1 < 14)
        {
            tiles = new[]
            {
                map.p1Map[currentCursor1x1[0]]
            };
            if(curPlayer == 0 && p1shipsLoc[currentCursor1x1[0]] == -1)
            {
                p1shipsLoc[currentCursor1x1[0]] = cur1x1;
                ships.PlaceShip(players.Player1Color, cur1x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
                //showRemain();
                smallText.SetText((14 - (cur1x1))+"/8");
            }
            else if(curPlayer == 1 && p2shipsLoc[currentCursor1x1[0]] == -1)
            {
                p2shipsLoc[currentCursor1x1[0]] = cur1x1;
                ships.PlaceShip(players.Player2Color, cur1x1++, ships.GetCoord(tiles)[0],
                    ships.GetCoord(tiles)[1], false);
            }
            else showInvalid();
        }
        if (cur1x1 == 14 && cur2x1 == 6 && cur2x2 == 2)
        {
            buttonGroup.SetActive(false);
            finishButton.SetActive(true);
            //showPlacedAllBoats();
        }
    }

    public void setSize(int s)
    {
        currentSizeMode = s;
        if (currentSizeMode == 0) map.HighlightAreaP1(currentCursor2x2);
        else if (currentSizeMode == 1 && rotate2x1 == 0) map.HighlightAreaP1(currentCursor2x1);
        else if (currentSizeMode == 1 && rotate2x1 == 1) map.HighlightAreaP1(currentCursor1x2);
        else if (currentSizeMode == 2) map.HighlightAreaP1(currentCursor1x1);
        //showRemain();
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

    public void ChangePlayer()
    {
        Reset();
        curPlayer = 1;
        if (players.Player1Color == 0) blackShips.SetActive(false);
        else if (players.Player1Color == 1) blueShips.SetActive(false);
        else if (players.Player1Color == 2) greenShips.SetActive(false);
        buttonGroup.SetActive(true);
        finishButton.SetActive(false);
        smallOutline.SetActive(false);
        medOutline.SetActive(false);
        largeOutline.SetActive(true);
        map.HighlightAreaP1(currentCursor2x2);
        prepPanel.SetActive(false);
        waitPrepPanel.SetActive(true);
        RandomizeBoats();
        map.HighlightAreaP1(new int[] {});
        players.HideP2Ships();
        StartCoroutine(CountdownGoToAttackPanel(6));
        Reset();
        
    }

    public void showRemain()
    {
        if (currentSizeMode == 0)
        {
            popUpText.SetText((2 - cur2x2) + " of 2 Large ships left");
        }
        else if (currentSizeMode == 1)
        {
            popUpText.SetText((4 - (cur2x1 - 2)) + " of 4 Medium ships left");
        }
        else if (currentSizeMode == 2)
        {
            popUpText.SetText((8 - (cur1x1 - 6)) + " of 8 Small ships left");
        }
        popUpText.color = Color.blue;
        StartCoroutine(Countdown(1));
    }

    public void showInvalid()
    {
        popUpText.SetText("Space is occupied by another boat");
        popUpText.color = Color.red;
        StartCoroutine(Countdown(2));
    }

    public void showPlacedAllBoats()
    {
        popUpText.SetText("You have placed all your boats! Press finish.");
        popUpText.color = Color.green;
        StartCoroutine(Countdown(3));
    }
    
    private IEnumerator Countdown(int s)
    {
        popUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds (s);
        popUpText.gameObject.SetActive(false);
    }

    private IEnumerator CountdownGoToAttackPanel(int s)
    {
        yield return new WaitForSeconds (s);
        waitPrepPanel.SetActive(false);
        attackPanel.SetActive(true);
        map.HighlightAreaP1(new int[] {cursor});
    }

    public void RandomizeBoats()
    {
        Random rnd = new Random();
        int x;
        while (cur2x2 < 2)
        {
            x = rnd.Next(0, 100);
            if (x % 10 == 9) x -= 1;
            if (x > 89) x -= 10;
            currentCursor2x2 = new int[] {x,x+1,x+10,x+11};
            currentSizeMode = 0;
            AddShip();
        }
        while (cur2x1 < 6)
        {
            x = rnd.Next(0, 90);
            currentCursor2x1 = new int[] {x,x+10};
            x = rnd.Next(0, 100);
            if (x % 10 == 9) x -= 1; 
            currentCursor1x2 = new int[] {x,x+1};
            currentSizeMode = 1;
            rotate2x1 = rnd.Next(0, 2);
            AddShip();
        }
        while (cur1x1 < 14)
        {
            x = rnd.Next(0, 100);
            currentCursor1x1 = new int[] {x};
            currentSizeMode = 2;
            AddShip();
        }
    }

    public void Reset()
    {
        currentSizeMode = 0;
        currentCursor1x1 = new int[] {0};
        currentCursor2x1 = new int[] {0,10};
        currentCursor1x2 = new int[] {0,1};
        currentCursor2x2 = new int[] {0,1,10,11};
        cur2x2 = 0;
        cur2x1 = 2;
        cur1x1 = 6;
        curPlayer = 0;
        rotate2x1 = 0;
    }

    public void AttackMove(int s)
    {
        if (s == 0)//up
        {
            if (cursor > 9) cursor -= 10;
        }
        if (s == 1)//left
        {
            if (cursor % 10 != 0) cursor -= 1;
        }
        if (s == 2)//right
        {
            if (cursor % 10 != 9) cursor += 1;
        }
        if (s == 3)//down
        {
            if (cursor < 90) cursor += 10;
        }
        map.HighlightAreaP1(new int[] {cursor});
    }

    public void Attack(int s)
    {
        int temp;
        if (s == 0)
        {
            if (p2shipsLoc[cursor] != -1)
            {
                CPUShips -= 1;
                shipInd1CPU.SetText(""+CPUShips);
                shipInd2CPU.SetText(""+CPUShips);
                temp = p2shipsLoc[cursor];
                for (int i = 0; i < 100; i++)
                {
                    if (p2shipsLoc[i] == temp) p2shipsLoc[i] = -1;
                }
                ships.showShip(players.Player2Color,temp);
                showParticle(0);
                hit.Play();
                if (CPUShips == 0)
                {
                    endText.SetText("YOU WON");
                    endText.color = Color.green;
                    attackPanel.SetActive(false);
                    endPanel.SetActive(true);
                }
                else
                {
                    popUpP1AttackText.SetText("HIT");
                    popUpP1AttackText.fontSize = 200;
                    popUpP1AttackText.color = Color.green;
                    StartCoroutine(CountdownP1(0, 2));
                }
            }
            else
            {
                popUpP1AttackText.SetText("MISS");
                popUpP1AttackText.fontSize = 200;
                popUpP1AttackText.color = Color.red;
                attackButton.SetActive(false);
                miss.Play();
                StartCoroutine(CountdownP1(1, 2));
            }
        }
        else if (s == 1)
        {
            if (p1shipsLoc[enemycursor] != -1)
            {
                CPUHitStreak += 1;
                playerShips -= 1;
                shipInd1P1.SetText(""+playerShips);
                shipInd2P1.SetText(""+playerShips);
                temp = p1shipsLoc[enemycursor];
                for (int i = 0; i < 100; i++)
                {
                    if (p1shipsLoc[i] == temp)
                    {
                        p1shipsLoc[i] = -1;
                        CPUcheck.Add(i);
                    }
                }
                ships.hideShip(players.Player1Color, temp);
                showParticle(1);
                hit.Play();
                if (playerShips == 0)
                {
                    endText.SetText("YOU LOST");
                    endText.color = Color.red;
                    waitAttackPanel.SetActive(false);
                    endPanel.SetActive(true);
                }
                else
                {
                    if (CPUHitStreak == 1) hitText.SetText("HIT");
                    else if (CPUHitStreak > 1) hitText.SetText("HITx"+CPUHitStreak);
                    StartCoroutine(CountdownCPUHit(2));
                    CPUAttack();
                }
            }
            else
            {
                CPUHitStreak = 0;
                CPUcheck.Add(enemycursor);
                waitAttackPanel.SetActive(false);
                attackPanel.SetActive(true);
                popUpP1AttackText.SetText("Your opponent missed! Your turn.");
                popUpP1AttackText.fontSize = 140;
                popUpP1AttackText.color = Color.green;
                attackButton.SetActive(true);
                miss.Play();
                StartCoroutine(CountdownP1(2, 3));
            }
        }
    }
    
    private IEnumerator CountdownP1(int f, int s)
    {
        popUpP1AttackText.gameObject.SetActive(true);
        if (f == 2)
        {
            if (players.Player2Color == 0) blackShips.SetActive(true);
            else if (players.Player2Color == 1) blueShips.SetActive(true);
            else if (players.Player2Color == 2) greenShips.SetActive(true);
        
            if (players.Player1Color == 0) blackShips.SetActive(false);
            else if (players.Player1Color == 1) blueShips.SetActive(false);
            else if (players.Player1Color == 2) greenShips.SetActive(false);
        }
        yield return new WaitForSeconds (s);
        popUpP1AttackText.gameObject.SetActive(false);
        if (f == 1) CPUAttack();
    }
    
    private IEnumerator CountdownCPUHit(int s)
    {
        yield return new WaitForSeconds (s);
        hitText.SetText("");
    }
    
    private IEnumerator CountdownCPU(int s)
    {
        yield return new WaitForSeconds (s);
        Random rnd = new Random();
        enemycursor = rnd.Next(0, 100);
        while (CPUcheck.Contains(enemycursor))
        {
            enemycursor = rnd.Next(0, 100);
        }
        Attack(1);
    }
    
    
    public void CPUAttack()
    {
        attackPanel.SetActive(false);
        waitAttackPanel.SetActive(true);
        
        if (players.Player2Color == 0) blackShips.SetActive(false);
        else if (players.Player2Color == 1) blueShips.SetActive(false);
        else if (players.Player2Color == 2) greenShips.SetActive(false);
        
        if (players.Player1Color == 0) blackShips.SetActive(true);
        else if (players.Player1Color == 1) blueShips.SetActive(true);
        else if (players.Player1Color == 2) greenShips.SetActive(true);
        StartCoroutine(CountdownCPU(5));
    }

    public void onClickHome()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void showParticle(int s)
    {
        float posx, posy;
        GameObject[] tiles = new[] {
            map.p1Map[cursor]
        };
        if (s == 1)
        {
            tiles = new[]
            {
                map.p1Map[enemycursor]
            };
        }
        posx = ships.GetCoord(tiles)[0];
        posy = ships.GetCoord(tiles)[1];

        particleSystem.transform.localPosition = new Vector3(posx, 0, posy);
        particleSystem.Play();
    }
}
