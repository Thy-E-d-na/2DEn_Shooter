using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] private GameObject textCenterScreen;
    [SerializeField] private TextMeshProUGUI textWave;
    private int currentWave = 1;
    private int spawnObjectCount = 0;

    [Header("Prefab")]
    [SerializeField] private List<GameObject> chickenFormationPrefabs;
    [SerializeField] private List<GameObject> asteroidPrefabs;
    [SerializeField] private GameObject bossPrefab;

    public static WaveController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(0.5f);
        textCenterScreen.SetActive(true);
        textWave.text = "Wave " + currentWave;
        yield return new WaitForSeconds(2.5f);
        textCenterScreen.SetActive(false);
        SpawnWave();
    }

    private void SpawnWave()
    {
        if (currentWave % 5 == 0)
        {
            spawnObjectCount = 1;
            Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
        }
        else if (currentWave % 10 == 3 || currentWave % 10 == 7)
        {
            spawnObjectCount = 25;
            StartCoroutine(SpawnAsteroids(spawnObjectCount));
        }
        else
        {
            int randomFormation = Random.Range(0, chickenFormationPrefabs.Count);
            if (randomFormation == 0) //Loai 1 co 40 con
            {
                spawnObjectCount = 40;
                Instantiate(chickenFormationPrefabs[randomFormation], Vector3.zero, Quaternion.identity);
            }
            else if (randomFormation == 1) //Loai 2 huy sau 12.5s
            {
                StartCoroutine(SpawnChickenFormation2());
            }
        }
    }

    public void NextWave()
    {
        currentWave++;
        StartCoroutine(StartWave());
    }

    private IEnumerator SpawnAsteroids(int numSpawn)
    {
        Vector3 spawnPos;
        int randomIndex;
        for (int i = 0; i < numSpawn; i++)
        {
            spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.1f, 0));
            spawnPos.z = 0;
            randomIndex = Random.Range(0, asteroidPrefabs.Count);
            Instantiate(asteroidPrefabs[randomIndex], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        }
    }

    private IEnumerator SpawnChickenFormation2()
    {
        spawnObjectCount = int.MaxValue;
        var formation = Instantiate(chickenFormationPrefabs[1], Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(12.5f);
        Destroy(formation);
        NextWave();
    }

    public void CheckNextWave()
    {
        spawnObjectCount--;
        if (spawnObjectCount <= 0)
        {
            NextWave();
        }
    }
}
