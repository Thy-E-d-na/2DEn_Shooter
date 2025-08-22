using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    [Header("Ship")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private List<GameObject> bulletList;
    [SerializeField] private int currentLvBullet;
    [SerializeField] private GameObject VFX;
    [SerializeField] private GameObject shield;

    [Header("Score")]
    [SerializeField] private int chickenLegScore = 200;

    private void Start()
    {
        StartCoroutine(DisableShield());
    }

    private void Update()
    {
        Move();
        Fire();

    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);
        transform.position += direction.normalized * speed * Time.deltaTime;

        Vector3 TopRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -TopRightPoint.x + 0.5f, TopRightPoint.x - 0.5f), Mathf.Clamp(transform.position.y, -TopRightPoint.y + 1.5f, TopRightPoint.y - 0.5f), 0);
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletList[currentLvBullet], transform.position, Quaternion.identity);
            AudioController.Instance.PlayFireClip();
        }
    }

    private IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(10f);
        shield.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shield.activeSelf && (collision.CompareTag("Chicken") || collision.CompareTag("Egg") || collision.CompareTag("Boss") || collision.CompareTag("Asteroids")))
            Destroy(gameObject);
        else if (collision.CompareTag("ChickenLeg"))
        {
            AudioController.Instance.PlayEatingClip();
            Destroy(collision.gameObject);
            ScoreController.Instance.GetScore(chickenLegScore);
        }
        else if (collision.CompareTag("Present"))
        {
            Destroy(collision.gameObject);
            currentLvBullet = Mathf.Clamp(currentLvBullet + 1, 0, bulletList.Count - 1);
            AudioController.Instance.PlayLvUpClip();
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            AudioController.Instance.PlayDeathClip();
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            ScoreController.Instance.HeartCountDown();
            if (ScoreController.Instance.heartCount >= 0)
            {
                ShipController.instance.SpawnShip();
            }
        }
    }
}
