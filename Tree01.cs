using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree01 : MonoBehaviour
{
    public GameObject decider;
    public GameObject sound;
    public GameObject meringue;
    public GameObject treePrime;
    float Waiting;
    bool overSprite;
    Vector3 mousePosition;
    Vector3 objPosition;
    bool isMobile;
    public int RipeLevel = -1;
    public Animator anim;
    public int treeNum;
    bool triggerGrowing;
    bool rotton;

    //long originalwaiting;
    //long waiting;

    public void InitiateCitrus(int treeNumber, int difficulty)
    {
        //originalwaiting = 30;
        //waiting = 0;
        Waiting = 1;
        triggerGrowing = false;
        overSprite = false;
        isMobile = false;
        rotton = false;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
        //Waiting = OriginalWait;
        treeNum = treeNumber;

        if (difficulty == 1)
        {
            SetEasyTrees();
        }
        else if (difficulty == 2)
        {
            SetHardTrees();
        }

        RipeLevel = -1;
    }

    
    IEnumerator DecayLemon()
    {
        //Lemon Decaying
        do
        {
            if (RipeLevel > 0)   //No need to wait if the lemon is picked (ripe/unripe) or turn rotton
            {
                yield return new WaitForSeconds(Waiting);
                if (RipeLevel > 0)
                {
                    RipeLevel--;
                }
            }
        }
        while (RipeLevel > 0);

        if (RipeLevel == 0)
        {
            sound.GetComponent<Sound>().Bad();
            anim.SetInteger("LemonStatus", RipeLevel);
            decider.GetComponent<Decider>().TotalErrors++;
            meringue.GetComponent<MeringueDance>().ErrorMinor();
            yield return new WaitForSeconds(Waiting);
        }

        yield return new WaitForSeconds(Waiting);
        if (RipeLevel > -7)   //yes I know this is dangerous
        {
            RipeLevel = -1;
            decider.GetComponent<Decider>().tree[treeNum] = false;  //free up the tree to make new lemon
            decider.GetComponent<Decider>().TreesBusy--;            //Reflect total trees busy
        }
    }
    

    public void StartGrowing()
    {
        RipeLevel = 6;
        StartCoroutine(DecayLemon());
        //waiting = originalwaiting;
        //triggerGrowing = true;
    }

    //Ripe Levels
    //-3 = Picked Ripe Lemon
    //-2 = Picked Unripe Lemon
    //-1 = No Lemon
    // 0 = Rotton Lemon
    // 1 = Ripe Lemon
    // 2 = Ripe Lemon
    // 3 = Ripe Lemon
    // 4 = Unripe Lemon
    // 5 = Unripe Lemon
    // 6 = Unripe Lemon

    /*
    void Update()
    {
        if (triggerGrowing)
        {
            if (RipeLevel > 0)
            {
                //if( )
                waiter();
            }
            else if (RipeLevel == 0)  //rotton
            {
                if (!rotton)
                {
                    sound.GetComponent<Sound>().Bad();
                    resetwaiter();
                }
                rotton = true;

                if (waiting == 0)
                {
                    RipeLevel = -1;
                    triggerGrowing = false;
                    rotton = false;
                }
                else
                {
                    waiting--;
                }

            }
            else if (RipeLevel == -2 || RipeLevel == -3)  //picked early/picked just right
            {
                if (waiting == 0)
                {
                    RipeLevel = -1;
                    triggerGrowing = false;
                }
                else
                {
                    waiting--;
                }
            }

            
        }
    }
    */

    //void waiter()
    //{
    //    if (waiting > 0)
    //    {
    //        waiting--;
    //    }
    //    else
    //    {
    //        RipeLevel--;
    //        resetwaiter();
    //    }
    //}

    //void resetwaiter()
    //{
    //    waiting = originalwaiting;
    //}
    

    void FixedUpdate()
    {
        if (isMobile)
        {
            MobileMove();
        }
        else
        {
            PCMove();
        }
    }

    void MobileMove()
    {
        //Set Visual
        anim.SetInteger("LemonStatus", RipeLevel);

        if (Input.touches.Length > 0)
        {
            mousePosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 10f);
            objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            overSprite = this.GetComponent<SpriteRenderer>().bounds.Contains(objPosition);

            if (overSprite)
            {

                //Not Meringue involved
                if (RipeLevel > 3 && RipeLevel < 7)  //unripe
                {
                    sound.GetComponent<Sound>().Bad();
                    RipeLevel = -2;
                    //resetwaiter();
                    //Set Visual
                    anim.SetInteger("LemonStatus", RipeLevel);
                    decider.GetComponent<Decider>().TotalErrors++;
                    meringue.GetComponent<MeringueDance>().ErrorMinor();
                }
                else if (RipeLevel > 0 && RipeLevel < 4) //ripe
                {
                    sound.GetComponent<Sound>().Good();
                    RipeLevel = -3;
                    //resetwaiter();
                    //Set Visual
                    anim.SetInteger("LemonStatus", RipeLevel);
                    decider.GetComponent<Decider>().TotalPicked++;
                }

                //Meringue involved (TO DO)
            }
        }
    }


    void PCMove()
    {
        //Set Visual
        anim.SetInteger("LemonStatus", RipeLevel);

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        overSprite = this.GetComponent<SpriteRenderer>().bounds.Contains(objPosition);
        if (overSprite && Input.GetMouseButtonDown(0))
        {

            //Not Meringue involved
            if (RipeLevel > 3 && RipeLevel < 7)  //unripe
            {
                sound.GetComponent<Sound>().Bad();
                RipeLevel = -2;
                //resetwaiter();
                //Set Visual
                anim.SetInteger("LemonStatus", RipeLevel);
                decider.GetComponent<Decider>().TotalErrors++;
                meringue.GetComponent<MeringueDance>().ErrorMinor();
            }
            else if (RipeLevel > 0 && RipeLevel < 4) //ripe
            {
                sound.GetComponent<Sound>().Good();
                RipeLevel = -3;
                //resetwaiter();
                //Set Visual
                anim.SetInteger("LemonStatus", RipeLevel);
                decider.GetComponent<Decider>().TotalPicked++;
            }

            //Meringue involved (TO DO)

        }
    }





    private void SetEasyTrees()
    {
        GameObject prime;
        switch (treeNum)   //says which tree should be where
        {
            
            case 0:
                transform.position = new Vector3(0f, 4f, 10f);
                prime = Instantiate(treePrime, new Vector3(0f, 3.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 1:
                transform.position = new Vector3(-3f, 2f, 10f);
                prime = Instantiate(treePrime, new Vector3(-3f, 1.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 2:
                transform.position = new Vector3(3f, 2f, 10f);
                prime = Instantiate(treePrime, new Vector3(3f, 1.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 3:
                transform.position = new Vector3(-6f, 0f, 10f);
                prime = Instantiate(treePrime, new Vector3(-6f, -.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 4:
                transform.position = new Vector3(6f, 0f, 10f);
                prime = Instantiate(treePrime, new Vector3(6f, -.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 5:
                transform.position = new Vector3(-3f, -2f, 10f);
                prime = Instantiate(treePrime, new Vector3(-3f, -2.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 6:
                transform.position = new Vector3(3f, -2f, 10f);
                prime = Instantiate(treePrime, new Vector3(3f, -2.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 7:
                transform.position = new Vector3(0f, -4f, 10f);
                prime = Instantiate(treePrime, new Vector3(0f, -4.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            default:
                break;
        }
    }

    private void SetHardTrees()
    {
        GameObject prime;
        switch (treeNum)   //says which tree should be where
        {
            case 0:
                transform.position = new Vector3(1.5f, 4f, 10f);
                prime = Instantiate(treePrime, new Vector3(1.5f, 3.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 1:
                transform.position = new Vector3(-1.5f, 4f, 10f);
                prime = Instantiate(treePrime, new Vector3(-1.5f, 3.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 2:
                transform.position = new Vector3(1.5f, -4f, 10f);
                prime = Instantiate(treePrime, new Vector3(1.5f, -4.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 3:
                transform.position = new Vector3(-1.5f, -4f, 10f);
                prime = Instantiate(treePrime, new Vector3(-1.5f, -4.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 4:
                transform.position = new Vector3(4f, 3f, 10f);
                prime = Instantiate(treePrime, new Vector3(4f, 2.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 5:
                transform.position = new Vector3(4f, -3f, 10f);
                prime = Instantiate(treePrime, new Vector3(4f, -3.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 6:
                transform.position = new Vector3(-4f, 3f, 10f);
                prime = Instantiate(treePrime, new Vector3(-4f, 2.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 7:
                transform.position = new Vector3(-4f, -3f, 10f);
                prime = Instantiate(treePrime, new Vector3(-4f, -3.5f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 8:
                transform.position = new Vector3(6.5f, 1.5f, 10f);
                prime = Instantiate(treePrime, new Vector3(6.5f, 1f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 9:
                transform.position = new Vector3(-6.5f, 1.5f, 10f);
                prime = Instantiate(treePrime, new Vector3(-6.5f, 1f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 10:
                transform.position = new Vector3(6.5f, -1.5f, 10f);
                prime = Instantiate(treePrime, new Vector3(6.5f, -2f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            case 11:
                transform.position = new Vector3(-6.5f, -1.5f, 10f);
                prime = Instantiate(treePrime, new Vector3(-6.5f, -2f, 10f), Quaternion.identity);
                prime.transform.localScale = new Vector3(.8f, .8f, 1);
                break;
            default:
                break;
        }
    }

    public void LemonSpeedUp()
    {
        Waiting = Waiting - .20f;
    }
}