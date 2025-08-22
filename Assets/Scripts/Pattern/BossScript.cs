using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private GameObject eggPrefab;
    [SerializeField] private int HP = 100;
    [SerializeField] private int score = 1000;
    [SerializeField] private GameObject VFX;

    public static BossScript Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(DropEgg());
        StartCoroutine(MoveRandom());
    }

    private IEnumerator DropEgg()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator MoveRandom()
    {
        Vector3 point = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0.5f, 1f), 0));
        point.z = 0;
        while (transform.position != point)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, 0.1f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        StartCoroutine(MoveRandom());
    }

    public void GetShot(int damage)
    {
        HP -= damage;
        AudioController.Instance.PlayChickenHurtClip();
        if (HP <= 0)
        {
            Destroy(gameObject);
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            ScoreController.Instance.GetScore(score);
            //InterstitialAd.Instance.ShowAd();
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            WaveController.Instance.CheckNextWave();
        }
    }
}
