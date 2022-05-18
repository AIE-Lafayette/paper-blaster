using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    public void PlayGame() 
    {
        Application.LoadLevel("play_scene");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
