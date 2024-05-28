using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour, IDamageable
{
    public GameObject ArrowPrefab;
    public float coolDown = 2.5f;
    private float timer;
    public Rigidbody2D ArcherRb;
    public float speed;
    public int maxHealth = 75;
    public int MaxHealth => maxHealth;
    public int cost = 125;

    public float cooldown = 1.5f;

    // Ajout d'une variable pour définir la portée du raycast
    public float raycastDistance = 5f;

    // Ajout d'une variable pour ajuster la position de départ du raycast
    public Vector2 raycastOffset = new Vector2(-1.1f, -1.9f); // Ajustez selon vos besoins

    void Start()
    {
        timer = coolDown;
    }

    // Update is called 50x per second
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        // Vérifier le tag et ajuster la direction et l'échelle en conséquence
        if (CompareTag("Player1"))
        {
            // Définir l'échelle et la direction pour Player1
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            ArcherRb.velocity = Vector2.right * speed;

            // Création d'un raycast pour détecter les ennemis de Player2
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3)raycastOffset, Vector2.right, raycastDistance);
            bool player2Detected = false;

            // Parcourir tous les objets touchés par le raycast
            foreach (RaycastHit2D hit in hits)
            {
                // Vérifier si l'objet touché est un ennemi (tag "Player2")
                if (hit.collider.CompareTag("Player2"))
                {
                    player2Detected = true;
                }
            }

            // Si un ennemi (tag "Player2") a été détecté, tirer une flèche
            if (player2Detected)
            {
                FireArrow();
            }
        }
        else if (CompareTag("Player2"))
        {
            // Définir l'échelle et la direction pour Player2
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            ArcherRb.velocity = Vector2.left * speed;

            // Création d'un raycast pour détecter les ennemis de Player1
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3)raycastOffset, Vector2.left, raycastDistance);
            bool player1Detected = false;

            // Parcourir tous les objets touchés par le raycast
            foreach (RaycastHit2D hit in hits)
            {
                // Vérifier si l'objet touché est un ennemi (tag "Player1")
                if (hit.collider.CompareTag("Player1"))
                {
                    player1Detected = true;
                }
            }

            // Si un ennemi (tag "Player1") a été détecté, tirer une flèche
            if (player1Detected)
            {
                FireArrow();
            }
        }
    }

    void FireArrow()
    {
        if (timer < 0)
        {
            // Ajustez la valeur Y pour que la flèche spawn plus haut que l'archer
            float spawnOffsetY = -1.9f; // Vous pouvez ajuster cette valeur selon vos besoins
            float spawnOffsetX = CompareTag("Player1") ? -1.1f : 1.1f; // Définir spawnOffsetX en fonction du tag

            Vector3 spawnPosition = transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0);

            GameObject arrow = Instantiate(ArrowPrefab, spawnPosition, Quaternion.identity);
            
            // Définir le tag du tireur de la flèche
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.shooterTag = gameObject.tag;
            }

            timer = coolDown;
        }
    }

    private void OnDestroy()
    {
        if (CompareTag("Player1"))
        {
            IAStats.AddMoney(145);
            IAStats.AddExp(165);
        }
        else if (CompareTag("Player2"))
        {
            PlayerStats.AddMoney(145);
            PlayerStats.AddExp(165);
        }
    }
}
