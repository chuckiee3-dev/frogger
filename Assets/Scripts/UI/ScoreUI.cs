using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private int goUpScore = 10;
    [SerializeField] private int fillSlotScore = 200;
    [SerializeField] private int levelCompleteScore = 1500;

    [SerializeField] private TextMeshProUGUI scoreTMP;

    private int _currentScore;

    private void Awake()
    {
        _currentScore = 0;
        scoreTMP.text = _currentScore.ToString();
    }

    private void OnEnable()
    {
        GameActions.onGameStart += ResetScore;
        GameActions.onPlayerFillSlot += IncreaseScoreFillSlot;
        GameActions.onLevelComplete += IncreaseScoreLevelComplete;
        PlayerActions.onGoUp += IncreaseScoreGoUp;
    }


    private void OnDisable()
    {
        GameActions.onGameStart -= ResetScore;
        GameActions.onPlayerFillSlot -= IncreaseScoreFillSlot;
        GameActions.onLevelComplete -= IncreaseScoreLevelComplete;
        PlayerActions.onGoUp -= IncreaseScoreGoUp;
    }
    private void IncreaseScoreGoUp()
    {
        AddScore(goUpScore);
    }
    private void IncreaseScoreFillSlot()
    {
        AddScore(fillSlotScore);
    }
    private void IncreaseScoreLevelComplete()
    {
        AddScore(levelCompleteScore);
    }
    private void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        scoreTMP.text = _currentScore.ToString();
        ScoreActions.ScoreUpdated(_currentScore);
    }
    
    private void ResetScore()
    {
        _currentScore = 0;
        scoreTMP.text = _currentScore.ToString();
    }
}
