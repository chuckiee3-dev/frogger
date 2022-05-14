using System;
using UnityEngine;

public static class GameActions
{
    public static Action onPlayerFillSlot;
    public static Action onPlayerLoseLife;
    
    public static void PlayerLoseLife()
    {
        Debug.Log("Lose life");
        onPlayerLoseLife?.Invoke();
    }

    public static void PlayerFillSlot()
    {
        Debug.Log("Player fill slot");
        onPlayerFillSlot?.Invoke();
    }
}
