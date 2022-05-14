using System;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    private Canvas _canvas;

    private void OnEnable()
    {
        GameActions.onLevelComplete += DisplayCanvas;
        GameActions.onLevelReloaded += HideCanvas;
    }
    private void OnDisable()
    {
        GameActions.onLevelComplete -= DisplayCanvas;
        GameActions.onLevelReloaded -= HideCanvas;
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
