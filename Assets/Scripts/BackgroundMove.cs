using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _transformSpeed;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        _mesh.material.mainTextureOffset += new Vector2(0, _transformSpeed * Time.deltaTime);
    }
    //[SerializeField] private float speed = 0.03f;

    //private Renderer render;
    //private void Start()
    //{
    //    render = GetComponent<Renderer>();
    //}

    //private void FixedUpdate()
    //{
    //    Vector2 offset = render.material.mainTextureOffset;
    //    offset.y += Time.fixedDeltaTime * speed;
    //    render.material.mainTextureOffset = offset;
    //}
}
