using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene("player_scene");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
