using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerScript : MonoBehaviour
{

    [SerializeField] private InputActionReference _fire;

    [SerializeField] private float _moveSpeed;
    public UnityEvent OnFire;
    private void Update()
    {
        Move();
        if (_fire.action.triggered)
        {
            Fire();
        }
    }
    private void Move()
    {
        var input = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(input);
        mousePos.z = 2;
        transform.position = Vector3.MoveTowards(transform.position,mousePos,_moveSpeed * Time.deltaTime);
       
    }
    public void Fire() => OnFire.Invoke();

}
