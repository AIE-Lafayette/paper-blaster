using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField] private Animator _bookAnimator;
    [SerializeField] private GameObject _canvas;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayGame()
    {
        _bookAnimator.SetTrigger("Open");
        _canvas.SetActive(false);
        RoutineBehaviour.Instance.StartNewTimedAction(args => SceneManager.LoadScene("play_scene"), TimedActionCountType.SCALEDTIME, 2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu_scene");
    }
}
