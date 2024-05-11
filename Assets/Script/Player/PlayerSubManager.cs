using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubManager : MonoBehaviour
{
    public bool boolOfPaused;


    public Vector3 vector3OfDesiredPosition;

    // Update is called once per frame
    void Update()
    {
        //Pause();

    }

    private void FixedUpdate()
    {

    }

    public void Pause()
    {
        if (!boolOfPaused)
        {


            // Check for attack input
            if (Input.GetKeyDown(KeyCode.Z))
            {

            }

            // Check for parry input
            if (Input.GetKeyDown(KeyCode.X))
            {

            }
        }

        // Check for pause input (e.g., the "P" key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        boolOfPaused = !boolOfPaused;

        // Pause or unpause the game based on the isPaused value
        Time.timeScale = boolOfPaused ? 0f : 1f;

        // Implement any additional pause menu UI or logic here
        // For example, showing/hiding a pause menu UI element
    }
   
   

}
