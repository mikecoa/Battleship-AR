using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public AudioSource open1;
    public AudioSource open2;

    public void PlayOpen1()
    {
        open1.Play();
    }
    
    public void PlayOpen2()
    {
        open2.Play();
    }
}
