using System;
using UnityEngine;

public static class GameActions
{
    public static Action onPlayerFillSlot;
    public static Action onPlayerLoseLife;
    public static Action onPlayerEarnLife;
    public static Action onPlayerRevive;
    public static Action<int> onPlayerLifeUpdated;
    public static Action onGameOver;
    public static Action onLevelComplete;
    public static Action onGameStart;
    public static Action onTimeOver;

    public static void PlayerFillSlot()
    {
        Debug.Log("Player fill slot");
        onPlayerFillSlot?.Invoke();
    }
    public static void PlayerEarnLife()
    {
        onPlayerEarnLife?.Invoke();
    }
    public static void PlayerLoseLife()
    {
        Debug.Log("Lose life");
        onPlayerLoseLife?.Invoke();
    }

    public static void PlayerRevive()
    {
        onPlayerRevive?.Invoke();
    }

    public static void PlayerLifeUpdated(int currentLives)
    {
        onPlayerLifeUpdated?.Invoke(currentLives);
    }
    public static void GameOver()
    {
        onGameOver?.Invoke();
    }

    public static void LevelComplete()
    {
        onLevelComplete?.Invoke();
    }

    public static void GameStart()
    {
        onGameStart?.Invoke();
    }

    public static void TimeOver()
    {
        onTimeOver?.Invoke();
    }
}
