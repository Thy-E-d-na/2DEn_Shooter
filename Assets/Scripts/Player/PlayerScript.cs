using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[SerializeField] private InputActionReference _move;
    [SerializeField] private float _moveSpeed;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        var input = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(input);
        mousePos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position,mousePos,_moveSpeed * Time.deltaTime);
       
    }
}
