using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    //A link to the book's animator
    [SerializeField] private Animator _bookAnimator;
    //The UI canvas
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Vector3 _firstPos;
    [SerializeField] private Vector3 _secondPos;
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject HighScoreTip;
    [SerializeField] private Text HighScoreText;
    private bool _animating;
    private int tempScore;
    private float temp;

    //Main menu switch  to play scene
    public void Play()
    {
        _animating = true;
        Time.timeScale = 1;
        _bookAnimator.SetTrigger("Open");
        _canvas.SetActive(false);
        RoutineBehaviour.Instance.StartNewTimedAction(args => SceneManager.LoadScene("play_scene"), 
        TimedActionCountType.SCALEDTIME, 1.3f);
    }

    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }

    //Quit the game to game over
    public void GameOver() 
    {
        PlayerPrefs.SetInt("Score", GameManagerBehavior.Score);
        Time.timeScale = 1;
        SceneManager.LoadScene("game_over_scene");
    }

    //Switch to main menu
    public void LoadMainMenu()
    {
        _animating = true;
        _bookAnimator.SetTrigger("Close");
        _canvas.SetActive(false);
        RoutineBehaviour.Instance.StartNewTimedAction(args => { SceneManager.LoadScene("main_menu_scene"); },
        TimedActionCountType.SCALEDTIME, 1.25f);
    }

    //Updating the camera zoom
    private void Update()
    {
        if(_animating == true)
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _secondPos, 0.0785f);

        temp += (0.75f * Time.deltaTime);
        tempScore = Mathf.RoundToInt(Mathf.Lerp(0, PlayerPrefs.GetInt("Score"), temp));
        if (ScoreText != null)
            ScoreText.text = "Score: " + tempScore;
        if (HighScoreText != null)
            HighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }

    private void Awake()
    {
        if(HighScoreTip != null)
            HighScoreTip.SetActive(false);
        tempScore = 0;
        temp = 0;
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("HighScore")) 
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
            if (HighScoreTip != null)
                HighScoreTip.SetActive(true);
        }
    }
}
