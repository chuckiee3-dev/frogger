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
    private int _scoreRequiredPerLife = 20000;
    private int _nextLifeRewardScore;
    private void Awake()
    {
        _currentScore = 0;
        scoreTMP.text = _currentScore.ToString();
        _nextLifeRewardScore = _scoreRequiredPerLife;
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
        CheckEarnedExtraLife();
        scoreTMP.text = _currentScore.ToString();
        ScoreActions.ScoreUpdated(_currentScore);
    }

    private void CheckEarnedExtraLife()
    {
        if (_currentScore < _nextLifeRewardScore) return;
        _nextLifeRewardScore += _scoreRequiredPerLife;
        GameActions.PlayerEarnLife();
    }

    private void ResetScore()
    {
        _currentScore = 0;
        scoreTMP.text = _currentScore.ToString();
    }
}
