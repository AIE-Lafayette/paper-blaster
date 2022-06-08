using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour
{
    //Score and difficulty
    public static int Score;
    public static int CurrentScore;
    private int _page;
    public static int CurrentPaperAmount;
    public static int CurrentStickerAmount;
    private bool _pageCheck;
    private int _stickerThreshold;
    public int Page 
    {
        get { return _page; }
    }

    [SerializeField] private GameObject pauseCanvas;
    public static bool pauseCheck;

    //Spawning variables
    [SerializeField] private SpawnerBehavior _spawner;
    private int _stickerSpawnSpeed;
    private int _paperSpawnAmount;
    private RoutineBehaviour.TimedAction _spawnStickerAction;
    [SerializeField] private Transform _playerTransform;
    private static AudioSource _audio;
    [SerializeField]
    private AudioSource _audioRef;

    public static void IncreaseScore(int value)
    {
        GameManagerBehavior.Score++;
        GameManagerBehavior.CurrentScore++;
    }

    void Start()
    {
        pauseCanvas.SetActive(false);
        pauseCheck = false;
        _page = 1;
        Score = 0;
        CurrentScore = 0;
        _paperSpawnAmount = 6;
        _stickerSpawnSpeed = 5;
        _stickerThreshold = 6;
        _spawnStickerAction = new RoutineBehaviour.TimedAction();
        PageSetup();
        _audio = _audioRef;
    }

    void Update()
    {
        GameLoop();
        pauseCanvas.SetActive(pauseCheck);
        //Debug.Log("Paper: " + CurrentPaperAmount);
        //Debug.Log("Sticker: " + CurrentStickerAmount);
    }

    void GameLoop()
    {
         if (CurrentPaperAmount == 0 && !_pageCheck && CurrentStickerAmount == 0)
        {
            Debug.Log("Board Cleared");
            _pageCheck = true;
            Score += 50;
            _paperSpawnAmount = 3 + _page;
            _stickerSpawnSpeed = 5 + Mathf.RoundToInt(_page / 2);
            RoutineBehaviour.Instance.StartNewTimedAction(args => { PageSetup(); _pageCheck = false; }, TimedActionCountType.SCALEDTIME, 3f);
            _page++;
        }
        if (CurrentScore > _stickerThreshold && CurrentPaperAmount > 0)
        {
            if (!_spawnStickerAction.IsActive)
            {
                _spawnStickerAction = RoutineBehaviour.Instance.StartNewTimedAction(args => _spawner.SpawnSticker(1), TimedActionCountType.SCALEDTIME, _stickerSpawnSpeed);
            }
        }
    }

    void PageSetup()
    {
        _spawner.SpawnPaper(_paperSpawnAmount);
        CurrentScore = 0;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        pauseCheck = true;
        _audio.Pause();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseCheck = false;
        _audio.UnPause();
    }
}
