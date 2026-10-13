using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float distance = 3f; // quanto sobe e desce a partir da posicao inicial

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

        // Patrulha vertical: vai e volta entre (inicio - distance) e (inicio + distance)
        // (o "+ distance" faz ele comecar no meio do caminho, sem dar um pulo)
        float offset = Mathf.PingPong(Time.time * speed + distance, distance * 2) - distance;
        rb.MovePosition(new Vector2(startPosition.x, startPosition.y + offset));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NewMonoBehaviourScript>() != null)
        {
            EndGame.Die();
        }
    }
}
