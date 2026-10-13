using UnityEngine;

public static class EndGame
{
    private static int collectableCount;
    private static bool initialized;

    public static bool IsGameOver()
    {
        return initialized && collectableCount == 0;
    }

    public static void Init()
    {
        collectableCount = GameObject.FindGameObjectsWithTag("Coletavel").Length;
        initialized = true;
    }

    public static void Collect()
    {
        if (initialized && collectableCount > 0)
        {
            collectableCount--;
        }
    }
}
