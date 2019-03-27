using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree00 : MonoBehaviour
{
    bool overSprite = false;
    bool isMobile = false;
    Vector2 objPosition;
    Vector2 guidePosition;
    int RipeLevel;

    void Start ()
    {
        RipeLevel = 0;
        overSprite = false;
        isMobile = false;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
        transform.position = new Vector3(0, 0, 0);
    }
	

	void Update ()
    {
        if (isMobile)
        {
            MobileTouch();
        }
        else
        {
            PCTouch();
        }
	}


    void MobileTouch()
    {
        if (Input.touches.Length > 0)
        {

        }
    }

    void PCTouch()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;


        if (Input.GetKeyDown("space"))
        {
            transform.position = objPosition;
        }
            

        if (overSprite )//&& Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touching");
        }

    }

}
