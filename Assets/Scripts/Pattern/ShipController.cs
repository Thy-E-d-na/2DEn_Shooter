using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private GameObject ship;

    public static ShipController instance;
    private void Awake()
    {
        instance = this;
    }

    public void SpawnShip()
    {
        var newShip = Instantiate(ship, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.8f, 0)), Quaternion.identity);
        var point = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.1f, 0));
        StartCoroutine(MoveShip(newShip, point));
    }

    private IEnumerator MoveShip(GameObject ship, Vector3 point)
    {
        float timer = 0;
        while (ship && ship.transform.position != point)
        {
            timer += Time.fixedDeltaTime;
            ship.transform.position = Vector3.Lerp(ship.transform.position, point, timer);
            yield return new WaitForFixedUpdate();
        }
    }
}
