using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public AudioClip Sound; // Clip de audio para el sonido de la bala
    private AudioSource audioSource; // Componente AudioSource

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>(); // Agrega el AudioSource al GameObject

        // Configura el AudioSource
        audioSource.clip = Sound; // Asigna el clip de audio
        audioSource.playOnAwake = false; // Evita que se reproduzca automáticamente
        audioSource.volume = 1f; // Ajusta el volumen según sea necesario

        // Reproduce el sonido de la bala
        audioSource.Play();

        // Destruye la bala automáticamente después de 2 segundos
        Destroy(gameObject, 2.0f);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si ha impactado con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Llama al método TakeHit() del enemigo
            EnemieScript enemy = collision.gameObject.GetComponent<EnemieScript>();
            if (enemy != null)
            {
                enemy.TakeHit(); // Llama al método para que el enemigo registre el impacto
            }
        }

        // Destruye la bala al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
