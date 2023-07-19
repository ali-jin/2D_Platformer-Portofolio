using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


// Return to the menu when the is finished
public class ReturnMenu : MonoBehaviour
{
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 0"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
