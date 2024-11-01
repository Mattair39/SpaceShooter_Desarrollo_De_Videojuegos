using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Prefab del enemigo
    public float spawnInterval = 3f;      // Intervalo entre cada aparici�n
    public int maxEnemies = 5;            // N�mero m�ximo de enemigos en pantalla
    public float spawnDistance = 15f;     // Distancia desde el centro donde aparecen los enemigos

    private float spawnTimer;

    void Update()
    {
        // Generar enemigos si no se ha alcanzado el l�mite m�ximo
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
        // Genera una posici�n aleatoria fuera del mapa
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        
    }
}
