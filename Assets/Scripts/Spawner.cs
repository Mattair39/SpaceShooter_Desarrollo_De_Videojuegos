using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Prefab del enemigo
    public float spawnInterval = 3f;      // Intervalo entre cada aparición
    public int maxEnemies = 5;            // Número máximo de enemigos en pantalla
    public float spawnDistance = 15f;     // Distancia desde el centro donde aparecen los enemigos

    private float spawnTimer;

    void Update()
    {
        // Generar enemigos si no se ha alcanzado el límite máximo
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        // Genera una posición aleatoria fuera del mapa
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        
    }
}
