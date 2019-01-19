using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : ModuleBase
{
    public AudioSource audiosource;
    public AudioClip[] clips;

    public void Play(int number)
    {
        audiosource.clip = clips[number];
        audiosource.Play();
    }
}
