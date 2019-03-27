using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeringueDance : MonoBehaviour
{
    public Animator anim;
    int MeringueDancePosition;
    int TotalErrors;
    bool MajorTriggerOn;
    bool MinorTriggerOn;
    bool MinorTriggerOn2;

    public void SetMeringue()
    {
        MajorTriggerOn = false;
        MinorTriggerOn = false;
        MinorTriggerOn2 = false;
        TotalErrors = 0;
        MeringueDancePosition = 0;
        transform.position = new Vector3(0f, 0f, 10f);
    }

    public void ErrorMinor()
    {
        TotalErrors++;
        if (TotalErrors == 1)
        {
            StartCoroutine(MinorTrigger());
        }
        else if (TotalErrors == 2)
        {
            StartCoroutine(MinorTrigger2());
        }
        else if (TotalErrors > 2) //3 errors
        {
            MajorTrigger();
        }
    }

    IEnumerator MinorTrigger()
    {
        MinorTriggerOn = true;
        yield return new WaitForSeconds(1);
        if (!MajorTriggerOn && !MinorTriggerOn2)
        {
            MinorTriggerOn = false;
            MeringueDancePosition = 0;
        }
    }

    IEnumerator MinorTrigger2()
    {
        MinorTriggerOn2 = true;
        yield return new WaitForSeconds(1);
        if (!MajorTriggerOn)
        {
            MinorTriggerOn = false;
            MinorTriggerOn2 = false;
            MeringueDancePosition = 0;
        }
    }

    void MajorTrigger()
    {
        MajorTriggerOn = true;
    }

    void Update()
    {
        //Set Visual
        anim.SetInteger("MeringueDancing", MeringueDancePosition);
        if (MajorTriggerOn)
        {
            MeringueDancePosition = 2;
        }
        else if (MinorTriggerOn || MinorTriggerOn2)
        {
            MeringueDancePosition = 1;
        }
        anim.SetInteger("MeringueDancing", MeringueDancePosition);
    }
}