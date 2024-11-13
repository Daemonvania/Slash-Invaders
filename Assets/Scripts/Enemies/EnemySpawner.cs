using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject enemyPrefab;
  
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float minWaitTime = 5f;
    [SerializeField] float maxWaitTime = 10f;
    
    private GameObject player;
    
    private bool isSpawning = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");   
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (!Application.isPlaying)
            return;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        // Wait for a random amount of time
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        Transform spawnPoint;
        // Keep looking for a valid spawn point until one is found farther than 10 units away from the player
        do
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            spawnPoint = spawnPoints[randomIndex];
        }
        while (Vector2.Distance(spawnPoint.position, player.transform.position) < 5f);

        // Instantiate the enemy at the valid spawn point
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Check if spawning should continue
        if (isSpawning)
        {
            SpawnEnemies();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
