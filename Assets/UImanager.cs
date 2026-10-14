using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public Image[] lifeIcons; // um peixinho por vida
    public TMP_Text endGameText;
    public float timeLimit = 30f;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource music;

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

        // Placar e vidas
        if (scoreText != null)
        {
            scoreText.text = "Peixes: " + EndGame.GetScore() + "/" + EndGame.GetTotal();
        }

        // Vida perdida fica com o icone apagado
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].color = i < EndGame.GetLives() ? Color.white : new Color(1f, 1f, 1f, 0.25f);
        }

        if (EndGame.IsGameOver())
        {
            gameOverShown = true;
            endGamePanel.SetActive(true);
            Time.timeScale = 0f;

            // Para a musica de fundo e toca o som de vitoria ou derrota
            if (music != null)
            {
                music.Stop();
            }

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
