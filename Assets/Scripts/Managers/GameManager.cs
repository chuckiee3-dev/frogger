using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalLives = 5;
    [SerializeField]private float levelLoadDelay = 2f;
    [SerializeField] private LevelComponent levelPrefab;
    private LevelComponent _currentLevel;
    private int _currentLives;
    private int _totalNumberOfSlots;
    private int _filledSlotCount;
    private bool _gameOver;
    private WaitForSeconds levelLoadWait;
    private void Awake()
    {
        _totalNumberOfSlots = GameObject.FindGameObjectsWithTag("Slot").Length;
        _currentLives = totalLives;
        _filledSlotCount = 0;
        levelLoadWait = new WaitForSeconds(levelLoadDelay);
        _currentLevel = FindObjectOfType<LevelComponent>();
    }

    private void Start()
    {
        GameActions.GameStart();
        GameActions.PlayerLifeUpdated(_currentLives);
    }

    private void OnEnable()
    {
        GameActions.onPlayerFillSlot += SlotFilled;
        GameActions.onPlayerLoseLife += PlayerLoseLife;
        GameActions.onTimeOver += PlayerLoseLife;
        GameActions.onGameRestart += RestartGame;
        PlayerActions.onDeathAnimationFinished += CheckForGameOver;
    }

    private void OnDisable()
    {
        GameActions.onPlayerFillSlot -= SlotFilled;
        GameActions.onPlayerLoseLife -= PlayerLoseLife;
        GameActions.onTimeOver -= PlayerLoseLife;
        GameActions.onGameRestart -= RestartGame;
        PlayerActions.onDeathAnimationFinished -= CheckForGameOver;
    }
    private void RestartGame()
    {
        if (_gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ReloadLevel()
    {
        StartCoroutine(DelayedLevelLoad());
    }

    private IEnumerator DelayedLevelLoad()
    {
        yield return levelLoadWait;
        if (_currentLevel)
        {
            Destroy(_currentLevel.gameObject);
        }
        _currentLevel = Instantiate(levelPrefab);
        _gameOver = false;
        GameActions.LevelReloaded();
    }

    private void PlayerEarnLife()
    {
        _currentLives++;
        GameActions.PlayerLifeUpdated(_currentLives);
    }
    private void PlayerLoseLife()
    {
        if(_currentLives == 0) return;
        _currentLives--;
        GameActions.PlayerLifeUpdated(_currentLives);
        
    }
    private void CheckForGameOver()
    {
        if (_currentLives == 0)
        {
            _gameOver = true;
            GameActions.GameOver();
        }
        else
        {
            GameActions.PlayerRevive();
        }
    }
    private void SlotFilled()
    {
        if(_filledSlotCount == _totalNumberOfSlots) return;
        _filledSlotCount++;
        if (_filledSlotCount == _totalNumberOfSlots)
        {
            ReloadLevel();
            GameActions.LevelComplete();
        }
        
    }
}
