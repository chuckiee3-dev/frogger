using System;

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
    public static void PlayerLoseLife()
    {
        onLoseLife?.Invoke();
    }

    
}
