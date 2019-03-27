using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    float scaleRate = 1.3f;
    float minScale = 0.1f;
    float maxScale = 10.0f;
    int coolDown;
    public bool StartGrowing;
    public bool StartShrinking;

    public void Start()
    {
        StartGrowing = true;
        StartShrinking = false;
        coolDown = 2;
    }

    
    

    // Update is called once per frame
    void Update()
    {
        //grow
        if (StartGrowing)
        {
            if (coolDown != 0)
            {
                coolDown--;
            }
            else
            {
                Growing();
            }
        }

        //shrink
        if (StartShrinking)
        {
            Shrinking();
        }
    }

    void Growing()
    {
        transform.localScale += Vector3.one * scaleRate;
        transform.position = new Vector3(0f, 0f, 10f);
        if (transform.localScale.x > maxScale)
        {
            StartGrowing = false;
        }
    }

    void Shrinking()
    {
        transform.position = new Vector3(0f, 0f, 10f);
        transform.localScale -= Vector3.one * scaleRate;

        if (transform.localScale.x < minScale)
        {
            transform.position = new Vector3(50f, 50f, 50f);
            StartShrinking = false;
            SceneManager.LoadScene("LemonHarvestScene");
        }
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void TriggerShrinking()
    {
        StartShrinking = true;
    }

    public void TriggerGrowing()
    {
        StartGrowing = true;
    }
}
