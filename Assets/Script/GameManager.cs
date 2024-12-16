using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Ensemble des ennemis qui peuvent apparaitre
    [SerializeField] private GameObject[] enemies;

    // Limite de la zone d'apparition
    [SerializeField] private int bounds = 48;

    // Ensemble des spawners et indicateur de celui a utiliser
    [SerializeField] private EnemySpawn[] spawners = new EnemySpawn[30];
    private int spawnerCounter = 0;

    // Ensemble des ennemis deja spawn
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] private int maxEnemies = 100;

    // Prefab des caisses
    [SerializeField] GameObject cratePrefab;
    
    void Start()
    {
        StartCoroutine("SpawningRoutine");
    }

    private IEnumerator SpawningRoutine()
    {
        // Wave 1
        GameState.currWave++;
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(5.0f);
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(5.0f);

        // Wave 2
        GameState.currWave++;
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++)
        {
            SpawnSingleEnemy(enemies[1], GetSpawnPosition(bounds));
        }
        yield return new WaitForSeconds(3.0f);
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(5.0f);

        // Premiere caisse
        SpawnCrate();
        
        // Wave 3
        GameState.currWave++;
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++)
        {
            SpawnSingleEnemy(enemies[1], GetSpawnPosition(bounds));
        }
        yield return new WaitForSeconds(2.0f);
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(2.0f);
        SpawnSingleEnemy(enemies[2], GetSpawnPosition(bounds));
        yield return new WaitForSeconds(2.0f);
        SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
        yield return new WaitForSeconds(2.0f);

        // Wave 4 and above
        StartCoroutine("SpawningRoutine4andAbove");
    }

    private IEnumerator SpawningRoutine4andAbove()
    {
        GameState.currWave++;
        int enemiesToSpawn = GameState.currWave * 10;

        // Spawn des ennemis de tier 0
        int enemies0 = enemiesToSpawn * Random.Range(30, 51)/100;
        enemiesToSpawn -= enemies0;
        while (enemies0 > 0)
        {
            SpawnMultipleEnemies(enemies[0], GetSwarmSpawnPosition(5, 3));
            enemies0 -= 5;
        }
        yield return new WaitForSeconds(2.0f);

        // Spawn des ennemis de tier 1
        int enemies1 = enemiesToSpawn * Random.Range(20, 41)/100;
        enemiesToSpawn -= enemies1;
        while (enemies1 > 0)
        {
            SpawnSingleEnemy(enemies[1], GetSpawnPosition(bounds));
            enemies1 -= 2;
        }
        yield return new WaitForSeconds(2.0f);

        // Spawn des ennemis de tier 2
        int enemies2 = enemiesToSpawn * Random.Range(30, 41)/100;
        enemiesToSpawn -= enemies2;
        while (enemies2 > 0)
        {
            SpawnSingleEnemy(enemies[2], GetSpawnPosition(bounds));
            enemies2 -= 5;
        }
        yield return new WaitForSeconds(2.0f);

        // Spawn des ennemis de tier 3
        int enemies3 = enemiesToSpawn * Random.Range(30, 41)/100;
        enemiesToSpawn -= enemies3;
        while (enemies3 > 0)
        {
            SpawnSingleEnemy(enemies[3], GetSpawnPosition(bounds));
            enemies3 -= 5;
        }
        yield return new WaitForSeconds(2.0f);

        // Spawn des ennemis de tier 4
        while (enemiesToSpawn > 0)
        {
            SpawnSingleEnemy(enemies[4], GetSpawnPosition(bounds));
            enemiesToSpawn -= 10;
        }
        yield return new WaitForSeconds(2.0f);

        // Apparition des caisses toutes les 3 vagues
        if (GameState.currWave % 3 == 0)
        {
            SpawnCrate();
        }

        //Passage a la vague suivante
        GameState.UpgradeEnemies();
        StartCoroutine("SpawningRoutine4andAbove");
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
    private void SpawnSingleEnemy(GameObject enemy, Vector3Int position)
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
    private void SpawnMultipleEnemies(GameObject enemy, Vector3Int[] positions)
    {
        foreach (Vector3Int position in positions)
        {
            SpawnSingleEnemy(enemy, position);
        }
    }

// Apparition des caisses d'armes
    public void SpawnCrate()
    {
        Instantiate(cratePrefab, GetSpawnPosition(bounds), Quaternion.identity);
    }

// Controle du nombre d'ennemis vivants
    public void EnemiesController(GameObject enemy)
    {
        spawnedEnemies.Add(enemy);
        if (spawnedEnemies.Count >= maxEnemies)
        {
            Destroy(spawnedEnemies[0]);
            spawnedEnemies.RemoveAt(0);
        }
    }
}
