using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private Transform spawnGrid;
    [SerializeField] private GameObject chickenPrefab;
    [SerializeField] private GameObject boss;
    [SerializeField] private List<GameObject> asteroidsPrefab;

    private float gridSize = 1;
    private Vector3 spawnPos;
    private int spawnObjectCount = 0;

    public static SpawnerScript Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        spawnPos.x += (gridSize / 2 + (width / 8));
        spawnPos.y -= gridSize;
        spawnPos.z = 0;

        //SpawnChicken(Mathf.FloorToInt(height / 2 / gridSize), Mathf.FloorToInt(width / 1.5f / gridSize));
        StartCoroutine(SpawnAsteroids());
    }

    private void SpawnChicken(int row, int numChicken)
    {
        float x = spawnPos.x;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < numChicken; j++)
            {
                spawnPos.x += gridSize;
                GameObject chicken = Instantiate(chickenPrefab, spawnPos, Quaternion.identity);
                chicken.transform.parent = spawnGrid;
                spawnObjectCount++;
            }
            spawnPos.x = x;
            spawnPos.y -= gridSize;
        }
    }

    // public void CheckBossPossible()
    // {
    //     chickenCount--;
    //     if (chickenCount <= 0)
    //     {
    //         boss.gameObject.SetActive(true);
    //     }
    // }

    private IEnumerator SpawnAsteroids()
    {
        for (int i = 0; i < 25; i++)
        {
            spawnObjectCount++;
            spawnPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.1f, 0));
            spawnPos.z = 0;
            int randomIndex=Random.Range(0, asteroidsPrefab.Count);
            Instantiate(asteroidsPrefab[randomIndex], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0f, 1.5f));
        }
    }
    
}
