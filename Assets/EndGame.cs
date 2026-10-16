using UnityEngine;

public static class EndGame
{
    private static int collectableCount;
    private static int totalCollectables;
    private static int lives;
    private static float lastHitTime;
    private static bool initialized;
    private static bool timeUp;
    private static bool dead;
    private static bool treasureFound;

    public const int MaxLives = 2;
    private const float InvincibleTime = 1f; // tempo sem levar dano depois de um hit

    public static bool IsGameOver()
    {
        return initialized && (collectableCount == 0 || timeUp || dead || treasureFound);
    }

    public static bool IsTimeUp()
    {
        return timeUp;
    }

    public static bool IsTreasureFound()
    {
        return treasureFound;
    }

    public static bool IsDead()
    {
        return dead;
    }

    public static int GetScore()
    {
        return totalCollectables - collectableCount;
    }

    public static int GetTotal()
    {
        return totalCollectables;
    }

    public static int GetLives()
    {
        return lives;
    }

    public static void Init()
    {
        collectableCount = GameObject.FindGameObjectsWithTag("Coletavel").Length;
        totalCollectables = collectableCount;
        lives = MaxLives;
        lastHitTime = -InvincibleTime;
        initialized = true;
        timeUp = false;
        dead = false;
        treasureFound = false;
    }

    public static void Collect()
    {
        if (initialized && collectableCount > 0)
        {
            collectableCount--;
        }
    }

    // Pegar o tesouro vence o jogo na hora
    public static void FindTreasure()
    {
        if (initialized && !IsGameOver())
        {
            treasureFound = true;
        }
    }

    public static void TimeUp()
    {
        if (initialized)
        {
            timeUp = true;
        }
    }

    // Tira uma vida; se acabarem as vidas, o jogador morre.
    // Retorna true se o jogador realmente levou dano.
    public static bool Hit()
    {
        if (!initialized || IsGameOver() || Time.time - lastHitTime < InvincibleTime)
        {
            return false;
        }

        lastHitTime = Time.time;
        lives--;
        if (lives <= 0)
        {
            Die();
        }
        return true;
    }

    public static void Die()
    {
        if (initialized && !IsGameOver())
        {
            dead = true;
        }
    }
}
