using System;
using UnityEngine;

public class FrogSlot : MonoBehaviour
{
    private bool _isFilled;
    [SerializeField] private SpriteRenderer frogFilledRenderer;

    private void Awake()
    {
        frogFilledRenderer.enabled = false;
    }

    public void TryFillSlot()
    {
        if (!_isFilled)
        {
            _isFilled = true;
            frogFilledRenderer.enabled = true;
            GameActions.PlayerFillSlot();
        }
        else
        {
            GameActions.PlayerLoseLife();
        }
    }
    
}
