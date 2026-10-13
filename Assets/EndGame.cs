using UnityEngine;

public static class EndGame
{
    private static int collectableCount;
    private static bool initialized;
    private static bool timeUp;
    private static bool dead;

    public static bool IsGameOver()
    {
        return initialized && (collectableCount == 0 || timeUp || dead);
    }

    public static bool IsTimeUp()
    {
        return timeUp;
    }

    public static bool IsDead()
    {
        return dead;
    }

    public static void Init()
    {
        collectableCount = GameObject.FindGameObjectsWithTag("Coletavel").Length;
        initialized = true;
        timeUp = false;
        dead = false;
    }

    public static void Collect()
    {
        if (initialized && collectableCount > 0)
        {
            collectableCount--;
        }
    }

    public static void TimeUp()
    {
        if (initialized)
        {
            timeUp = true;
        }
    }

    public static void Die()
    {
        if (initialized && !IsGameOver())
        {
            dead = true;
        }
    }
}
