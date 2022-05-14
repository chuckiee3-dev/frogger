using System;
using UnityEngine;

public static class ScoreActions
{
    public static Action<int> onScoreUpdated;
    public static void ScoreUpdated(int newValue)
    {
        onScoreUpdated?.Invoke(newValue);
    }
}
