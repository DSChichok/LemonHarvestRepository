using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    string easyScore = "";
    string hardScore = "";
    public Text easytext;
    public Text hardtext;

    // Use this for initialization
    void Start ()
    {
        transform.position = new Vector3(0f, 0f, 100f);
        DisplayScores();
    }

    public void DisplayScores()
    {
        //Debug.Log(Application.persistentDataPath + "/HighScore.lcs");
        string path = Application.persistentDataPath + "/HighScore.lcs";
        if (!File.Exists(path))
        {
            StreamWriter sw = System.IO.File.CreateText(path);
            sw.Close();
            System.IO.File.WriteAllText(path, "0\n0");
            easyScore = "0";
            hardScore = "0";
        }
        else
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            if (lines[1] != null)
            {
                easyScore = string.Format("{0:n0}", System.Convert.ToInt32(lines[0]));
                hardScore = string.Format("{0:n0}", System.Convert.ToInt32(lines[1]));
            }
            else
            {
                //file is corrupt, recreate it
                StreamWriter sw = System.IO.File.CreateText(path);
                sw.Close();
                System.IO.File.WriteAllText(path, "0\n0");
                easyScore = "0";
                hardScore = "0";
            } 
        }

        easytext.text = easyScore;
        hardtext.text = hardScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LemonHarvestScene");
    }

    public void GoToTutorial()
    {
        transform.position = new Vector3(20f, 20f, 100f);
    }

    public void ComeBack()
    {
        transform.position = new Vector3(0f, 0f, 100f);
    }
}
