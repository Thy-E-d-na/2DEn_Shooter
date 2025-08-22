using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.03f;

    private Renderer render;
    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        Vector2 offset = render.material.mainTextureOffset;
        offset.y += Time.fixedDeltaTime * speed;
        render.material.mainTextureOffset = offset;
    }
}
