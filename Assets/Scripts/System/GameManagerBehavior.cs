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
    private bool _startCheck;
    public int Page 
    {
        get { return _page; }
    }

    //Pause menu
    [SerializeField] private GameObject pauseCanvas;
    public static bool pauseCheck;

    //Spawning variables
    [SerializeField] private SpawnerBehavior _spawner;
    private int _stickerSpawnSpeed;
    private int _paperSpawnAmount;
    private RoutineBehaviour.TimedAction _spawnStickerAction;
    [SerializeField] private Transform _playerTransform;

    //Audio
    private static AudioSource _audio;
    [SerializeField]
    private AudioSource _audioRef;

    //Increase the score of the game by one
    public static void IncreaseScore(int value)
    {
        GameManagerBehavior.Score += 100;
        GameManagerBehavior.CurrentScore++;
    }


    //Default variables on game start
    void Start()
    {
        _startCheck = true;
        pauseCanvas.SetActive(false);
        pauseCheck = false;
        _page = 1;
        Score = 0;
        CurrentScore = 0;
        _paperSpawnAmount = 6;
        _stickerSpawnSpeed = 5;
        _stickerThreshold = 6;
        CurrentPaperAmount = 0;
        CurrentStickerAmount = 0;
        _spawnStickerAction = new RoutineBehaviour.TimedAction();
        PageSetup();
        _audio = _audioRef;
        RoutineBehaviour.Instance.StartNewTimedAction(args => _startCheck = false, TimedActionCountType.UNSCALEDTIME, 1f);
    }

    void Update()
    {
        GameLoop();
        pauseCanvas.SetActive(pauseCheck);
    }

    void GameLoop()
    {
        // If the board is clear of all paper and stickers, increase difficulty and page count.
         if (CurrentPaperAmount == 0 && !_pageCheck && CurrentStickerAmount == 0 && !_startCheck)
        {
            _pageCheck = true;
            Score += 500;
            _paperSpawnAmount = 3 + _page;
            _stickerSpawnSpeed = 5 + Mathf.RoundToInt(_page / 2);
            RoutineBehaviour.Instance.StartNewTimedAction(args => { PageSetup(); }, TimedActionCountType.SCALEDTIME, 3f);
            RoutineBehaviour.Instance.StartNewTimedAction(args => { _pageCheck = false; }, TimedActionCountType.SCALEDTIME, 4f);
            _page++;
        }

        // If the sticker threshold has been met and if the sticker count isnt past the maximum stickers that can be in a scene, spawn stickers.
        if (CurrentScore > _stickerThreshold && CurrentPaperAmount > 0)
        {
            if (!_spawnStickerAction.IsActive && CurrentStickerAmount < _page)
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

    // Pause the game
    public static void PauseGame()
    {
        Time.timeScale = 0;
        pauseCheck = true;
        _audio.Pause();
    }


    //Unpauses the game
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseCheck = false;
        _audio.UnPause();
    }
}
