using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private GameObject[] _bullet;
    [SerializeField] private int _index;
    Vector3 screenEdge;
    private void Start()
    {
        float h, w;
        h = Camera.main.orthographicSize * 2;
        w = h * Camera.main.aspect;
        screenEdge = new Vector3(h, w,0);
    }
    public void Fire()
    {

        Instantiate(_bullet[_index],_firePos.position, Quaternion.identity);
        
    }
}
