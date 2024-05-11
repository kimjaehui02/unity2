using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeFixed : MonoBehaviour
{
    const int screenWidth = 1080;
    const int screenHeight = 1920;
    // Start is called before the first frame update
    void Start()
    {
        ChangeScreenSize();
    }

    // Update is called once per frame
    void ChangeScreenSize()
    {
        if(Screen.width> screenWidth)
        {
            Screen.SetResolution(screenHeight,screenWidth , false);
        }
        else
        {
            Screen.SetResolution(screenHeight, screenWidth, true);
        }
    }
}
