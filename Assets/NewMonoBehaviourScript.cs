using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audio;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EndGame.IsGameOver())
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!EndGame.IsGameOver() && other.gameObject.activeSelf && other.CompareTag("Coletavel"))
        {
            // Desativa imediatamente para nao contar o mesmo item duas vezes.
            other.gameObject.SetActive(false);
            if (audio != null)
            {
                audio.Play();
            }
            EndGame.Collect();
            Destroy(other.gameObject);
        }
    }
}


