using System;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    private void Update()
    {
        if (_canvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                HideCanvas();
                GameActions.RestartGame();
            }
        }
    }

    private void OnEnable()
    {
        GameActions.onGameOver += DisplayCanvas;
        GameActions.onGameStart += HideCanvas;
    }
    private void OnDisable()
    {
        GameActions.onGameOver -= DisplayCanvas;
        GameActions.onGameStart -= HideCanvas;
    }


    private void DisplayCanvas()
    {
        if (_canvas)
        {
            _canvas.enabled = true;
        }
    }
    private void HideCanvas()
    {
        if (_canvas)
        {
            _canvas.enabled = false;
        }
    }
}
