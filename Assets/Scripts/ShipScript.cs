using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float speed;
    public float rotationSpeed;
    public int lives; // N�mero de vidas configurables desde el Inspector
    public AudioClip Sound; // Clip de audio para la m�sica
    private AudioSource audioSource; // Componente AudioSource

    private Rigidbody2D Rigidbody2D;
    private float LastShoot;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource

        // Reproduce la m�sica al iniciar
        if (Sound != null)
        {
            audioSource.clip = Sound; // Asigna el clip de audio
            audioSource.loop = true; // Habilita el bucle para que la m�sica se repita
            audioSource.Play(); // Comienza a reproducir la m�sica
        }
    }

    void Update()
    {
        // Captura de entrada de movimiento usando las teclas WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Movimiento
        Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
        Rigidbody2D.velocity = movement;

        // Disparo al hacer clic con el bot�n izquierdo del mouse, con tiempo de enfriamiento
        if (Input.GetMouseButton(0) && Time.time > LastShoot + 0.20f)
        {
            Shoot();
            LastShoot = Time.time;
        }

        // Rotaci�n hacia el mouse
        RotateTowardsMouse();
    }

    public void TakeDamage()
    {
        lives--; // Resta una vida cada vez que se recibe un impacto

        if (lives <= 0)
        {
            DestroyShip(); // Destruye la nave si las vidas llegan a cero
        }
    }

    private void DestroyShip()
    {
        Debug.Log("Nave destruida. Fin del juego.");
        Destroy(gameObject); // Destruye la nave y finaliza el juego
        DieAndRestart();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Llama a TakeDamage() si el objeto que colisiona es un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    private void Shoot()
    {
        Vector3 direction = transform.up;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 1.5f, transform.rotation);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void RotateTowardsMouse()
    {
        // Obtiene la posici�n del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Asegura que el mouse y el jugador est�n en el mismo plano Z

        // Calcula la direcci�n desde el jugador hacia el mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Calcula el �ngulo en Z hacia el mouse
        float targetAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Interpola suavemente el �ngulo de rotaci�n Z
        float smoothedAngleZ = Mathf.LerpAngle(transform.eulerAngles.z, targetAngleZ, rotationSpeed * Time.deltaTime);

        // Aplica solo la rotaci�n en Z sin cambiar la posici�n
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngleZ);
    }

    private void DieAndRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
