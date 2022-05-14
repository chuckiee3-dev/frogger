using System;
using UnityEngine;

public static class PlayerActions
{
    public static Action onMovementStart;
    public static Action onMovementEnd;
    public static Action onLoseLife;
    public static Action onGoUp;
    public static Action onDeathAnimationFinished;

    public static void MovementStart()
    {
        onMovementStart?.Invoke();
    }
    public static void MovementEnd()
    {
        onMovementEnd?.Invoke();
    }
    public static void LoseLife()
    {
        onLoseLife?.Invoke();
    }
    
    public static void GoUp()
    {
        onGoUp?.Invoke();
    }


    public static void DeathAnimationFinished()
    {
        onDeathAnimationFinished?.Invoke();
    }
}
