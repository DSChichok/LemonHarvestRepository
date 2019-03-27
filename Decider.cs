using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Decider : MonoBehaviour
{
    public GameObject music;
    public GameObject sound;
    public int TotalErrors;
    public int TotalPicked;
    public int TreesBusy;
    bool GameStart = false;
    public bool[] tree;
    System.Random r;
    int TreeChoice;
    float OriginalWait = 1f;
    float Waiting;
    public Text m_MyText;
    public Text m_MyText2;
    int difficultyChoice;
    int totalBusyTrees;
    bool triggerFaster = false;

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

    int easyScore = 0;
    int hardScore = 0;

    public void DeciderStart(int difficulty)
    {
        string path = Application.persistentDataPath + "/HighScore.lcs";
        string[] lines = System.IO.File.ReadAllLines(path);
        easyScore = System.Convert.ToInt32(lines[0]);
        hardScore = System.Convert.ToInt32(lines[1]);

        triggerFaster = false;
        TotalErrors = 0;
        TotalPicked = 0;
        GameStart = true;
        tree = new bool[12];
        tree[0] = false;
        tree[1] = false;
        tree[2] = false;
        tree[3] = false;
        tree[4] = false;
        tree[5] = false;
        tree[6] = false;
        tree[7] = false;
        tree[8] = false;
        tree[9] = false;
        tree[10] = false;
        tree[11] = false;
        r = new System.Random();
        TreeChoice = -1;
        Waiting = OriginalWait;
        difficultyChoice = difficulty;

        if (difficultyChoice == 1)
        {
            totalBusyTrees = 8;
        }
        else if (difficultyChoice == 2)
        {
            totalBusyTrees = 12;
        }

        StartCoroutine(ChooseTree());
    }


    IEnumerator ChooseTree()
    {
        do
        {
            if (TreesBusy != totalBusyTrees)  //You can't start a tree if all trees are busy
            {
                do
                {
                    TreeChoice = r.Next(0, totalBusyTrees);   //choose a tree
                }
                while (tree[TreeChoice]);        //and make sure the tree isn't already busy

                tree[TreeChoice] = true;         //signify the tree is about to be busy

                GetTreeStarted();                //once you got your tree, start growing a lemon on it

                if (GameStart)                   //no point in waiting if the game has ended
                {
                    yield return new WaitForSecondsRealtime(Waiting);   //Wait a little before starting another tree
                }
            }
        }
        while (GameStart);
    }

    IEnumerator StartGameOverSequence()
    {
        string path = Application.persistentDataPath + "/HighScore.lcs";
        if (difficultyChoice == 1)
        {
            if (easyScore < TotalPicked)
            {
                easyScore = TotalPicked;
            }
        }
        else if (difficultyChoice == 2)
        {
            if (hardScore < TotalPicked)
            {
                hardScore = TotalPicked;
            } 
        }
        System.IO.File.WriteAllText(path, easyScore + "\n" + hardScore);

        music.GetComponent<Music>().KeepGoing = false;
        music.GetComponent<Music>().StopTheMusic();
        //Debug.Log(tree00.GetComponent<Tree01>().treeNum + " " + tree00.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree01.GetComponent<Tree01>().treeNum + " " + tree01.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree02.GetComponent<Tree01>().treeNum + " " + tree02.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree03.GetComponent<Tree01>().treeNum + " " + tree03.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree04.GetComponent<Tree01>().treeNum + " " + tree04.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree05.GetComponent<Tree01>().treeNum + " " + tree05.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree06.GetComponent<Tree01>().treeNum + " " + tree06.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree07.GetComponent<Tree01>().treeNum + " " + tree07.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree08.GetComponent<Tree01>().treeNum + " " + tree08.GetComponent<Tree01>().RipeLevel);
        //Debug.Log(tree09.GetComponent<Tree01>().treeNum + " " + tree09.GetComponent<Tree01>().RipeLevel);
        yield return new WaitForSecondsRealtime(.5f);
        tree00.GetComponent<Tree01>().RipeLevel = -9;
        tree01.GetComponent<Tree01>().RipeLevel = -9;
        tree02.GetComponent<Tree01>().RipeLevel = -9;
        tree03.GetComponent<Tree01>().RipeLevel = -9;
        tree04.GetComponent<Tree01>().RipeLevel = -9;
        tree05.GetComponent<Tree01>().RipeLevel = -9;
        tree06.GetComponent<Tree01>().RipeLevel = -9;
        tree07.GetComponent<Tree01>().RipeLevel = -9;
        tree08.GetComponent<Tree01>().RipeLevel = -9;
        tree09.GetComponent<Tree01>().RipeLevel = -9;
        tree10.GetComponent<Tree01>().RipeLevel = -9;
        tree11.GetComponent<Tree01>().RipeLevel = -9;
        sound.GetComponent<Sound>().Ouchies();
        yield return new WaitForSecondsRealtime(sound.GetComponent<Sound>().OhNo.length);
        //kill all trees
        var clones = GameObject.FindGameObjectsWithTag("TreePrimeLayer");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        SceneManager.LoadScene("GameOverScene");
    }

    // Update is called once per frame
    void Update()
    {
        m_MyText.text = TotalPicked.ToString();

        if (TotalErrors == 0)
        {
            m_MyText2.text = "Misses: ";
        }
        else if (TotalErrors == 1)
        {
            m_MyText2.text = "Misses: X";
        }
        else if (TotalErrors == 2)
        {
            m_MyText2.text = "Misses: XX";
        }
        else if (TotalErrors > 2)
        {
            m_MyText2.text = "Misses: XXX";
        }


        if (GameStart)
        {
            //Too many errors done, stop the game, it's game over bro
            if (TotalErrors > 2)
            {
                GameStart = false;
                StartCoroutine(StartGameOverSequence());
            }
        }

        //speed up gates
        if (TotalPicked == 50 && !triggerFaster)
        {
            triggerFaster = true;
            SpeedUp();
        }
        if (TotalPicked == 51 && triggerFaster)
        {
            triggerFaster = false;
        }

        if (TotalPicked == 100 && !triggerFaster)
        {
            triggerFaster = true;
            SpeedUp();
        }
        if (TotalPicked == 101 && triggerFaster)
        {
            triggerFaster = false;
        }

        if (TotalPicked == 150 && !triggerFaster)
        {
            triggerFaster = true;
            SpeedUp();
        }
        if (TotalPicked == 151 && triggerFaster)
        {
            triggerFaster = false;
        }

    }

    private void GetTreeStarted()
    {
        TreesBusy++; //Signifying a tree is about to start

        switch (TreeChoice)   //says which tree should be where
        {
            case 0:
                tree00.GetComponent<Tree01>().StartGrowing();
                break;
            case 1:
                tree01.GetComponent<Tree01>().StartGrowing();
                break;
            case 2:
                tree02.GetComponent<Tree01>().StartGrowing();
                break;
            case 3:
                tree03.GetComponent<Tree01>().StartGrowing();
                break;
            case 4:
                tree04.GetComponent<Tree01>().StartGrowing();
                break;
            case 5:
                tree05.GetComponent<Tree01>().StartGrowing();
                break;
            case 6:
                tree06.GetComponent<Tree01>().StartGrowing();
                break;
            case 7:
                tree07.GetComponent<Tree01>().StartGrowing();
                break;
            case 8:
                tree08.GetComponent<Tree01>().StartGrowing();
                break;
            case 9:
                tree09.GetComponent<Tree01>().StartGrowing();
                break;
            case 10:
                tree10.GetComponent<Tree01>().StartGrowing();
                break;
            case 11:
                tree11.GetComponent<Tree01>().StartGrowing();
                break;
            default:
                break;
        }
    }

    private void SpeedUp()
    {
        Waiting = Waiting - .20f;
        tree00.GetComponent<Tree01>().LemonSpeedUp();
        tree01.GetComponent<Tree01>().LemonSpeedUp();
        tree02.GetComponent<Tree01>().LemonSpeedUp();
        tree03.GetComponent<Tree01>().LemonSpeedUp();
        tree04.GetComponent<Tree01>().LemonSpeedUp();
        tree05.GetComponent<Tree01>().LemonSpeedUp();
        tree06.GetComponent<Tree01>().LemonSpeedUp();
        tree07.GetComponent<Tree01>().LemonSpeedUp();
        tree08.GetComponent<Tree01>().LemonSpeedUp();
        tree09.GetComponent<Tree01>().LemonSpeedUp();
        tree10.GetComponent<Tree01>().LemonSpeedUp();
        tree11.GetComponent<Tree01>().LemonSpeedUp();
    }
}
