using UnityEngine;

// Camera segue o jogador sem mostrar nada fora dos limites do mapa.
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minPosition; // canto inferior esquerdo do mapa
    public Vector2 maxPosition; // canto superior direito do mapa
    public float smooth = 6f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position = Clamp(target.position);
    }

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = Vector3.Lerp(transform.position, Clamp(target.position), smooth * Time.unscaledDeltaTime);
    }

    Vector3 Clamp(Vector3 p)
    {
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;
        float x = Mathf.Clamp(p.x, minPosition.x + halfW, Mathf.Max(minPosition.x + halfW, maxPosition.x - halfW));
        float y = Mathf.Clamp(p.y, minPosition.y + halfH, Mathf.Max(minPosition.y + halfH, maxPosition.y - halfH));
        return new Vector3(x, y, transform.position.z);
    }
}
