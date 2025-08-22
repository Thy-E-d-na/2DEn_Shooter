using System.Collections;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private GameObject chickenLegPrefab;
    [SerializeField] private GameObject presentPrefab;
    [SerializeField] private GameObject VFX;
    [SerializeField] private int score = 100;

    private void Awake()
    {
        StartCoroutine(DropEgg());
    }

    private IEnumerator DropEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 10f));
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
            AudioController.Instance.PlayChickenLayClip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            AudioController.Instance.PlayChickenDeathClip();
            DropPresent();
            Destroy(collision.gameObject);
            Instantiate(chickenLegPrefab, transform.position, Quaternion.identity);
            ScoreController.Instance.GetScore(score);
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 0.4f);
        }
    }

    private void OnDestroy()
    {
        //SpawnerScript.Instance.CheckBossPossible();
        if(gameObject.scene.isLoaded)
        {
            WaveController.Instance.CheckNextWave();
        }
    }

    private void DropPresent()
    {
        int dropRate = Random.Range(0, 100);
        if (dropRate > 90)
        {
            var persent=Instantiate(presentPrefab, transform.position, Quaternion.identity);
            Destroy(persent, 10f);
        }
    }
}
