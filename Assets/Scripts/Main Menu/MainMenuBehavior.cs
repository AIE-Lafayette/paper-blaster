using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    [SerializeField]
    private AudioSource _music;

    public void PlayGame() 
    {
        SceneManager.LoadScene("play_scene");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
