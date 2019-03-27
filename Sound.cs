using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip Count;
    public AudioClip Go;
    public AudioClip OhNo;
    public AudioClip GetLemon;
    public AudioClip WrongLemon;
    public AudioSource audioSource;

    public void CountSound()
    {
        audioSource.clip = Count;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void GoSound()
    {
        audioSource.clip = Go;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void Ouchies()
    {
        audioSource.clip = OhNo;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void Good()
    {
        audioSource.clip = GetLemon;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void Bad()
    {
        audioSource.clip = WrongLemon;
        audioSource.loop = false;
        audioSource.Play();
    }


    // Update is called once per frame
    void Update ()
    {
		
	}
}
