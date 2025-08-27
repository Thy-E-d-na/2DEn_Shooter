using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceToDestroy = 10f;
    [SerializeField] private int _dmg;
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        DestroyBulletOutScreen();
    }

    private void DestroyBulletOutScreen()
    {
        Vector3 centerScreen = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Vector3.Distance(centerScreen, transform.position) > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            BossScript.Instance.GetShot(_dmg);
            Destroy(gameObject);
        }
    }

}
