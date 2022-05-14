using System;
using UnityEngine;

public static class PlayerActions
{
    public static Action onMovementStart;
    public static Action onMovementEnd;
    public static Action onLoseLife;

    public static void MovementStart()
    {
        onMovementStart?.Invoke();
    }
    public static void MovementEnd()
    {
        onMovementEnd?.Invoke();
    }

    
}
