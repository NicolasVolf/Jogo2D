using UnityEngine;

// Bau do tesouro no fim da caverna: quem pegar vence o jogo na hora.
public class Treasure : MonoBehaviour
{
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        // Pulsa de leve para chamar atencao
        transform.localScale = baseScale * (1f + 0.08f * Mathf.Sin(Time.time * 4f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NewMonoBehaviourScript>() != null)
        {
            EndGame.FindTreasure();
        }
    }
}
