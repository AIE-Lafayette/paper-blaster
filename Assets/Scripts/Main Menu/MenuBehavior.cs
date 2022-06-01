using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    //A link to the book's animator
    [SerializeField] private Animator _bookAnimator;
    //The UI canvas
    [SerializeField] private GameObject _canvas;

    public void Play()
    {
        Time.timeScale = 1;
        _bookAnimator.SetTrigger("Open");
        _canvas.SetActive(false);
        RoutineBehaviour.Instance.StartNewTimedAction(args => SceneManager.LoadScene("play_scene"), 
        TimedActionCountType.SCALEDTIME, 2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu_scene");
    }
}
