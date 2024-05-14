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
        ArcherRb.velocity = Vector2.right * speed;

        // Création d'un raycast
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

    void FireArrow()
    {
        if (timer < 0)
        {
            // Ajustez la valeur Y pour que la flèche spawn plus haut que l'archer
            float spawnOffsetY = -1.9f; // Vous pouvez ajuster cette valeur selon vos besoins
            float spawnOffsetX = -1.1f;

            Vector3 spawnPosition = transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0);

            Instantiate(ArrowPrefab, spawnPosition, Quaternion.identity);
            timer = coolDown;
        }
    }
}
