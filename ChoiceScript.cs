using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceScript : MonoBehaviour
{
    public GameObject startgun;
    float scaleRate = 1.3f;
    float minScale = 0.1f;
    float maxScale = 10.0f;
    int coolDown;
    public bool StartGrowing;
    public bool StartShrinking;
    int easyHardChoice;

    public void Start()
    {
        transform.position = new Vector3(0f, 0f, 10f);
        StartShrinking = false;
        easyHardChoice = 0;  // 1 is easy, 2 is hard
    }

    void Update()
    {
        //shrink
        if (StartShrinking)
        {
            Shrinking();
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

            //Start the game
            startgun.GetComponent<StarGun>().StartTheGame(easyHardChoice);
        }
    }

    public void TriggerShrinkingEasy()
    {
        StartShrinking = true;
        easyHardChoice = 1;
    }

    public void TriggerShrinkingHard()
    {
        StartShrinking = true;
        easyHardChoice = 2;
    }
}
