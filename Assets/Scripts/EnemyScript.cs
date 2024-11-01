using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieScript : MonoBehaviour
{
    public float speed = 3f;
    public int hits = 2; // Número de impactos necesarios para destruir al enemigo
    public Transform playerTransform;
    

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // Destruye al enemigo al chocar con el jugador
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeHit(); // Llama al método para manejar el impacto
            Destroy(collision.gameObject); // Destruye la bala tras el impacto
        }
    }

    public void TakeHit()
    {
        hits--; // Resta un impacto

        if (hits <= 0)
        {
            Destroy(gameObject); // Destruye al enemigo si ha recibido suficientes impactos
        }
    }
}
