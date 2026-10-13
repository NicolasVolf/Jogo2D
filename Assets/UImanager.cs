using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject endGamePanel;
    private bool gameOverShown;

    void Start()
    {
        Time.timeScale = 1f;
        EndGame.Init();
        gameOverShown = false;
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOverShown && EndGame.IsGameOver())
        {
            gameOverShown = true;
            endGamePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
