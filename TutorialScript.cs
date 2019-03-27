using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}

    public void ReturnToTitle()
    {
        transform.position = new Vector3(20f, 20f, 100f);
    }

    public void ComeToTheFront()
    {
        transform.position = new Vector3(0f, 0f, 100f);
    }
}
