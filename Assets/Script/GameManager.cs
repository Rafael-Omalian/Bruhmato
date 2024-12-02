using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Ensemble des ennemis qui peuvent apparaitre
    [SerializeField] private GameObject[] enemies;

    // Limite de la zone d'apparition
    [SerializeField] private int bounds = 48;

    // Ensemble des spawners et indicateur de celui a utiliser
    [SerializeField] private EnemySpawn[] spawners = new EnemySpawn[30];
    private int spawnerCounter = 0;
    
    void Start()
    {
        InvokeRepeating("SpawningRoutine", 1f, 1f);
    }

    private void SpawningRoutine()
    {
        int random = Random.Range(0, 100);

        switch (random)
        {
            case <= 50:
                SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
                break;
            case <= 75:
                SpawnSingleEnemy(enemies[1], GetSpawnPosition(bounds));
                break;
            case <= 100:
                SpawnSingleEnemy(enemies[2], GetSpawnPosition(bounds));
                break;
            default:
                SpawnSingleEnemy(enemies[0], GetSpawnPosition(bounds));
                break;
        }
    }

// Fonction qui genere une nouvelle position dans un carre de cote range*2
    private Vector3Int GetSpawnPosition(int range)
    {
        return new Vector3Int(Random.Range(-range, range+1), 0, Random.Range(-range, range+1));
    }

// Fonction d'attribution des positions aux ennemis qui apparaissent en groupe
    private Vector3Int[] GetSwarmSpawnPosition(int number, int range)
    {
        // Initialisation du stockage des positions
        Vector3Int[] spawnPoints = new Vector3Int[number];

        // Calcule des positions
        Vector3Int centerPoint = new Vector3Int(Random.Range(-bounds + range, bounds+1 - range), 0, Random.Range(-bounds + range, bounds+1 - range));

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3Int newPoint = centerPoint + GetSpawnPosition(range);

            // Faire en sorte que les ennemis ne spawn pas au meme endroit
            int breaker = 0;
            while (spawnPoints.Contains(newPoint) && breaker < 20)
            {
                newPoint = centerPoint + GetSpawnPosition(range);
                breaker++;
            }

            spawnPoints[i] = newPoint;
        }

        return spawnPoints;
    }

// Fonction d'apparition d'un ennemi seul
    public void SpawnSingleEnemy(GameObject enemy, Vector3Int position)
    {
        spawners[spawnerCounter].transform.position = position;
        spawners[spawnerCounter].enemyPrefab = enemy;
        spawners[spawnerCounter].gameObject.SetActive(true);
        spawnerCounter++;
        if (spawnerCounter >= spawners.Length)
        {
            spawnerCounter = 0;
        }
    }
// Fonction d'apparition d'un groupe d'ennemis
    public void SpawnMultipleEnemies(GameObject enemy, Vector3Int[] positions)
    {
        foreach (Vector3Int position in positions)
        {
            SpawnSingleEnemy(enemy, position);
        }
    }
}
