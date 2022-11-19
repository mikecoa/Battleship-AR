using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource open1, open2, select, confirm1, confirm2, close1;


    public void PlayOpen1()
    {
        open1.Play();
    }
    
    public void PlayOpen2()
    {
        open2.Play();
    }
    public void PlaySelect()
    {
        select.Play();
    }
    public void PlayConfirm1()
    {
        confirm1.Play();
    }
    public void PlayConfirm2()
    {
        confirm2.Play();
    }

    public void PlayClose1()
    {
        close1.Play();
    }
}
