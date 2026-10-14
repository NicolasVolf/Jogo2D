using UnityEngine;
using TMPro;

public class UImanager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TMP_Text timerText;
    public TMP_Text endGameText;
    public float timeLimit = 30f;
    public AudioClip winSound;
    public AudioClip loseSound;

    private bool gameOverShown;
    private float elapsedTime;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1f;
        EndGame.Init();
        gameOverShown = false;
        elapsedTime = 0f;
        endGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverShown)
        {
            return;
        }

        // Cronometro: conta o tempo e mostra quanto falta
        if (!EndGame.IsGameOver())
        {
            elapsedTime += Time.deltaTime;
            float remaining = Mathf.Max(0f, timeLimit - elapsedTime);

            if (timerText != null)
            {
                timerText.text = "Tempo: " + remaining.ToString("F1");
            }

            if (remaining <= 0f)
            {
                EndGame.TimeUp();
            }
        }

        if (EndGame.IsGameOver())
        {
            gameOverShown = true;
            endGamePanel.SetActive(true);
            Time.timeScale = 0f;

            // Som de vitoria ou derrota
            if (audioSource != null)
            {
                audioSource.PlayOneShot(EndGame.IsDead() || EndGame.IsTimeUp() ? loseSound : winSound);
            }

            if (endGameText != null)
            {
                if (EndGame.IsDead())
                {
                    endGameText.text = "Você morreu!";
                }
                else if (EndGame.IsTimeUp())
                {
                    endGameText.text = "Tempo esgotado!";
                }
                else
                {
                    endGameText.text = "Você venceu!\nTempo final: " + elapsedTime.ToString("F1") + "s";
                }
            }
        }
    }
}
