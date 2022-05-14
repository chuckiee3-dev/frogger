using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiScoreUI : MonoBehaviour
{
    private int _currentHiScore;
    private static string hiScoreKey = "HiScore";
    [SerializeField] private TextMeshProUGUI hiScoreTMP;
    private void Awake()
    {
        _currentHiScore = PlayerPrefs.GetInt(hiScoreKey, 0);
        hiScoreTMP.text = _currentHiScore.ToString();
    }

    private void CheckIfHiScoreIsPassedAndUpdate(int newScore)
    {
        if(_currentHiScore > newScore) return;
        _currentHiScore = newScore;
        hiScoreTMP.text = _currentHiScore.ToString();
        PlayerPrefs.SetInt(hiScoreKey, _currentHiScore);
        PlayerPrefs.Save();
    }
    private void OnEnable()
    {
        ScoreActions.onScoreUpdated += CheckIfHiScoreIsPassedAndUpdate;
    }

    private void OnDisable()
    {
        ScoreActions.onScoreUpdated -= CheckIfHiScoreIsPassedAndUpdate;
    }

}
