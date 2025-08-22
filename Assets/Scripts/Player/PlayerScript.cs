using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private InputActionReference _move;
    [SerializeField] private float _moveSpeed;
    private void Update()
    {
        var input = _move.action.ReadValue<Vector2>();
        transform.position = Vector2.MoveTowards(transform.position, input, _moveSpeed * Time.deltaTime);
    }
}
