using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Spawn Setting")]
    public GameObject Enemy;
    public float initialSpawnRate = 0.01f;
    public float spawnRateIncrease = 0.1f;
    [SerializeField] private float currentSpawnRate;
    public bool EnableSpawn = true;
    public bool RESpawn = false;
    public enum SpawnSize // *
    {
        Top,
        Right,
        Buttom,
        Left
    }
    public SpawnSize spawnSize;
    public void ResetSpawnArea()
    {
        EnableSpawn = true;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {

        while (EnableSpawn)
        {                      
            // Spawn the object
            if(spawnSize == SpawnSize.Left) Instantiate(Enemy, new Vector3(-13,Random.Range(-9,5)), Quaternion.identity);
            if (spawnSize == SpawnSize.Right) Instantiate(Enemy, new Vector3(13, Random.Range(-9, 5)), Quaternion.identity);
            if (spawnSize == SpawnSize.Top) Instantiate(Enemy, new Vector3(Random.Range(-12,19),9), Quaternion.identity);
            if (spawnSize == SpawnSize.Buttom) Instantiate(Enemy, new Vector3(Random.Range(-12, 19), -9), Quaternion.identity);

            // Increase the spawn rate
            currentSpawnRate -= spawnRateIncrease;           
            yield return new WaitForSeconds(currentSpawnRate);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
       if(RESpawn)
       {
            RESpawn = false;
            ResetSpawnArea();            
       }
    }
}
