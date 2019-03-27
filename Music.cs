using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip Starting;
    public AudioClip Looping;
    public AudioClip Death;
    public AudioSource audioSource;
    public bool KeepGoing;

    public void PlayStarting()
    {
        audioSource.clip = Looping;
        audioSource.loop = true;
        audioSource.Play();
    }


    public void StopTheMusic()
    {
        audioSource.Stop();
    }

    public void PlayDeath()
    {
        audioSource.clip = Death;
        audioSource.loop = true;
        audioSource.Play();
    }
}
