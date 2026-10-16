using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float distance = 3f; // quanto anda para cada lado a partir da posicao inicial
    public Vector2 direction = Vector2.up; // up = patrulha vertical, right = horizontal

    private Rigidbody2D rb;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
    }

    void FixedUpdate()
    {
        if (EndGame.IsGameOver())
        {
            return;
        }

        // Patrulha: vai e volta entre (inicio - distance) e (inicio + distance) na direcao escolhida
        // (o "+ distance" faz ele comecar no meio do caminho, sem dar um pulo)
        float offset = Mathf.PingPong(Time.time * speed + distance, distance * 2) - distance;
        rb.MovePosition(startPosition + direction.normalized * offset);
    }

    // Stay: se o jogador continuar encostando, perde outra vida depois do tempo de invencibilidade
    private void OnTriggerStay2D(Collider2D other)
    {
        NewMonoBehaviourScript player = other.GetComponent<NewMonoBehaviourScript>();
        if (player != null)
        {
            player.TakeDamage();
        }
    }
}
