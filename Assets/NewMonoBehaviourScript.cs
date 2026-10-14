using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audio;
    SpriteRenderer sprite;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
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

        // Controle (Xbox/joystick): analogico esquerdo e D-pad
        if (Gamepad.current != null)
        {
            movement += Gamepad.current.leftStick.ReadValue();
            movement += Gamepad.current.dpad.ReadValue();
        }

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    // Chamado pelo inimigo quando encosta no jogador
    public void TakeDamage()
    {
        if (EndGame.Hit())
        {
            StartCoroutine(Blink());
        }
    }

    // Pisca em vermelho durante o tempo de invencibilidade (1 segundo)
    IEnumerator Blink()
    {
        for (int i = 0; i < 5; i++)
        {
            sprite.color = new Color(1f, 0.3f, 0.3f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
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


