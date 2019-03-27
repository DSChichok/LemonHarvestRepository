using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGun : MonoBehaviour
{
    int Stage;
    int difficulty;
    float waitTime;
    public Animator anim;
    public GameObject sound;
    public GameObject music;
    public GameObject decider;
    public GameObject tree00;
    public GameObject tree01;
    public GameObject tree02;
    public GameObject tree03;
    public GameObject tree04;
    public GameObject tree05;
    public GameObject tree06;
    public GameObject tree07;
    public GameObject tree08;
    public GameObject tree09;
    public GameObject tree10;
    public GameObject tree11;
    public GameObject md;

    // Use this for initialization
    public void StartTheGame(int difficultyChoice)
    {
        //Place all citrus before anything
        tree00.GetComponent<Tree01>().InitiateCitrus(0, difficultyChoice);
        tree01.GetComponent<Tree01>().InitiateCitrus(1, difficultyChoice);
        tree02.GetComponent<Tree01>().InitiateCitrus(2, difficultyChoice);
        tree03.GetComponent<Tree01>().InitiateCitrus(3, difficultyChoice);
        tree04.GetComponent<Tree01>().InitiateCitrus(4, difficultyChoice);
        tree05.GetComponent<Tree01>().InitiateCitrus(5, difficultyChoice);
        tree06.GetComponent<Tree01>().InitiateCitrus(6, difficultyChoice);
        tree07.GetComponent<Tree01>().InitiateCitrus(7, difficultyChoice);
        tree08.GetComponent<Tree01>().InitiateCitrus(8, difficultyChoice);
        tree09.GetComponent<Tree01>().InitiateCitrus(9, difficultyChoice);
        tree10.GetComponent<Tree01>().InitiateCitrus(10, difficultyChoice);
        tree11.GetComponent<Tree01>().InitiateCitrus(11, difficultyChoice);
        md.GetComponent<MeringueDance>().SetMeringue();
        difficulty = difficultyChoice;

        //Initialize and begin
        waitTime = 1f;
        Stage = 5;
        StartCoroutine(ClickClickBoom()); //Let's rock (triggers the start of the game)
    }

    IEnumerator ClickClickBoom()
    {
        //For Stage:
        //5 = display 3
        //4 = display 2
        //3 = display 1
        //2 = display Go!
        //1 = actually trigger the game to start (and move the Go! away from the screen)

        transform.position = new Vector3(0f, 0f, 10f);
        sound.GetComponent<Sound>().CountSound();      //Sound for 3
        yield return new WaitForSeconds(waitTime);

        Stage--;
        sound.GetComponent<Sound>().CountSound();      //Sound for 2
        yield return new WaitForSeconds(waitTime);

        Stage--;
        sound.GetComponent<Sound>().CountSound();      //Sound for 1
        yield return new WaitForSeconds(waitTime);

        Stage--;
        sound.GetComponent<Sound>().GoSound();         //Sound for Go!
        yield return new WaitForSeconds(waitTime);

        Stage--;                                       //actually trigger the game to start (and move the Go! away from the screen)
        music.GetComponent<Music>().PlayStarting();
        decider.GetComponent<Decider>().DeciderStart(difficulty);
        transform.position = new Vector3(50f, 50f, 50f);

    }

    // Update is called once per frame
    void Update ()
    {
        //Set Visual
        anim.SetInteger("Stage", Stage);
    }
}
