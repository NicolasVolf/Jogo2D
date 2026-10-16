using UnityEngine;

// Moreia que guarda o tesouro: fica parada na toca e persegue o jogador
// quando ele entra na area dela. Se o jogador sai, ela volta para casa.
public class Moray : MonoBehaviour
{
    public float speed = 3.5f;
    public Transform player;
    public Collider2D territory; // area da caverna onde ela persegue

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 home;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        home = rb.position;
    }

    void FixedUpdate()
    {
        if (EndGame.IsGameOver() || player == null)
        {
            return;
        }

        Vector2 target = territory.OverlapPoint(player.position) ? (Vector2)player.position : home;
        Vector2 next = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        // O sprite olha para a direita; vira quando anda para a esquerda
        if (Mathf.Abs(next.x - rb.position.x) > 0.001f)
        {
            sprite.flipX = next.x < rb.position.x;
        }
        rb.MovePosition(next);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        NewMonoBehaviourScript p = other.GetComponent<NewMonoBehaviourScript>();
        if (p != null)
        {
            p.TakeDamage();
        }
    }
}
