using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalLives = 5;
    private int _currentLives;
    private int _totalNumberOfSlots;
    private int _filledSlotCount;
    private void Awake()
    {
        _totalNumberOfSlots = GameObject.FindGameObjectsWithTag("Slot").Length;
        _currentLives = totalLives;
        _filledSlotCount = 0;
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
        PlayerActions.onDeathAnimationFinished += CheckForGameOver;
    }
    private void OnDisable()
    {
        GameActions.onPlayerFillSlot -= SlotFilled;
        GameActions.onPlayerLoseLife -= PlayerLoseLife;
        GameActions.onTimeOver -= PlayerLoseLife;
        PlayerActions.onDeathAnimationFinished -= CheckForGameOver;
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
            GameActions.LevelComplete();
        }
        
    }
}
