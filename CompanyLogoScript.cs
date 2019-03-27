using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanyLogoScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0f, 0f, 10f);
        StartCoroutine(PlayTitle());
    }

    IEnumerator PlayTitle()
    {
        yield return new WaitForSeconds(4);
        //start title scene
        SceneManager.LoadScene("TitleScene");
    }

}
