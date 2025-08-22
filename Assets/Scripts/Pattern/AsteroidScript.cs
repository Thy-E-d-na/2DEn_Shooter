using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] private float distanceToDestroy = 15f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject VFX;
    [SerializeField] private int score = 300;

    private void Update()
    {
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
        DestroyAsteroidOutScreen();
    }

    private void DestroyAsteroidOutScreen()
    {
        Vector3 centerScreen = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Vector3.Distance(centerScreen, transform.position) > distanceToDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            ScoreController.Instance.GetScore(score);
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 0.4f);
            WaveController.Instance.CheckNextWave();
        }
    }
}
