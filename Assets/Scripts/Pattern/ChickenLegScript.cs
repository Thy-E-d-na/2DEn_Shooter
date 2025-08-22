using UnityEngine;

public class ChickenLegScript : MonoBehaviour
{
    [SerializeField] private float distanceToDestroy = 10f;
    [SerializeField] private float forceStrength = 3f;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Fall();
    }

    private void Fall()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
        rb.AddForce(randomDirection * forceStrength, ForceMode2D.Impulse);
        //Tao luc xoay cho chicken leg
        rb.AddTorque(-randomDirection.x * forceStrength * 2, ForceMode2D.Impulse);
    }

    private void Update()
    {
        DestroyChickenLegOutScreen();
    }

    private void DestroyChickenLegOutScreen()
    {
        Vector3 centerScreen = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Vector3.Distance(centerScreen, transform.position) > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
