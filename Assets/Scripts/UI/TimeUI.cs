using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private float startTime = 60;
    [SerializeField] private float lowWarningTime = 10;
    
    [Header("Components")]
    [SerializeField] private Image fillImg;
    [SerializeField] private AudioSource audioSource;

    private Color _defaultColor;
    private readonly Color _lowColor = Color.red; 
    
    private float _timer;
    private bool _timerActive;
    private bool _shouldPlayWarningSfx;
    private void Awake()
    {
        _defaultColor = fillImg.color;
        ResetTimer();
    }

    private void Update()
    {
        if(!_timerActive) return;
        _timer -= Time.deltaTime;
        
        if (_timer <= 0)
        {
            StopTimer();
            GameActions.TimeOver();
            return;
        }
        
        fillImg.fillAmount = _timer / startTime;
        
        if (_shouldPlayWarningSfx && _timer <= lowWarningTime)
        {
            PlayerHasLowTime();
        }
    }

    private void PlayerHasLowTime()
    {
        _shouldPlayWarningSfx = false;
        audioSource.Play();
        fillImg.color = _lowColor;
    }

    private void StartTimer()
    {
        _timerActive = true;
    }
    private void OnEnable()
    {
        GameActions.onPlayerRevive += ResetTimer;
        GameActions.onPlayerLoseLife += StopTimer;
        GameActions.onGameStart += StartTimer;
    }


    private void OnDisable()
    {
        GameActions.onPlayerRevive -= ResetTimer;
        GameActions.onPlayerLoseLife -= StopTimer;
        GameActions.onGameStart -= StartTimer;
    }

    private void ResetTimer()
    {
        _timer = startTime;
        _shouldPlayWarningSfx = true;
        _timerActive = false;
        fillImg.color = _defaultColor;
    }

    private void StopTimer()
    {
        _timerActive = false;
        _shouldPlayWarningSfx = false;
    }
}
