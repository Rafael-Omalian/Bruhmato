using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Ensemble des ennemis qui peuvent apparaitre
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int bounds = 48;
    [SerializeField] private EnemySpawn[] spawners = new EnemySpawn[30];
    private List<EnemySpawn> unusedSpawners;
    private List<EnemySpawn> usedSpawners;
    
    void Start()
    {
        unusedSpawners = spawners.ToList();
        StartCoroutine(SpawningRoutine());
    }

    // Update is called once per frame
    private IEnumerator SpawningRoutine()
    {
        Vector3Int[] swarmSpawning = SwarmSpawnPoints(5, 3);

        foreach (Vector3Int spawn in swarmSpawning)
        {
            unusedSpawners[0].transform.position = spawn;
            unusedSpawners[0].enemyPrefab = enemies[0];
            unusedSpawners[0].gameObject.SetActive(true);
            unusedSpawners.RemoveAt(0);
        }

        yield return new WaitForSeconds(2f);
    }

    private Vector3Int[] SwarmSpawnPoints(int number, int range)
    {
        // Initialisation du stockage des positions
        Vector3Int[] spawnPoints = new Vector3Int[number];

        // Calcule des positions
        Vector3Int centerPoint = new Vector3Int(Random.Range(-bounds + range, bounds+1 - range), 0, Random.Range(-bounds + range, bounds+1 - range));

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3Int newPoint = centerPoint + GetNewPosition(range);

            // Faire en sorte que les ennemis ne spawn pas au meme endroit
            int breaker = 0;
            while (spawnPoints.Contains(newPoint) && breaker < 20)
            {
                newPoint = centerPoint + GetNewPosition(range);
                breaker++;
            }

            spawnPoints[i] = newPoint;
        }

        return spawnPoints;
    }

    private Vector3Int GetNewPosition(int range)
    {
        return new Vector3Int(Random.Range(-range, range+1), 0, Random.Range(-range, range+1));
    }
}
