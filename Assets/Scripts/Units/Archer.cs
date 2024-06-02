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
    public float maxHealth = 75;
    public float MaxHealth => maxHealth;
    public float damage = 15;
    public int cost = 125;

    public float cooldown = 1.5f;
    public float raycastDistance = 5f;

    public Vector2 raycastOffset = new Vector2(-1.1f, -1.9f); 

    void Start()
    {
        timer = coolDown;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (CompareTag("Player1"))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            ArcherRb.velocity = Vector2.right * speed;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3)raycastOffset, Vector2.right, raycastDistance);
            bool player2Detected = false;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("Player2"))
                {
                    player2Detected = true;
                }
            }

            if (player2Detected)
            {
                FireArrow();
            }
        }
        else if (CompareTag("Player2"))
        {

            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            ArcherRb.velocity = Vector2.left * speed;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (Vector3)raycastOffset, Vector2.left, raycastDistance);
            bool player1Detected = false;

            foreach (RaycastHit2D hit in hits)
            {

                if (hit.collider.CompareTag("Player1"))
                {
                    player1Detected = true;
                }
            }

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
            float spawnOffsetY = -1.9f; 
            float spawnOffsetX = CompareTag("Player1") ? -1.1f : 1.1f; 

            Vector3 spawnPosition = transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0);

            GameObject arrow = Instantiate(ArrowPrefab, spawnPosition, Quaternion.identity);
            
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                arrowScript.shooterTag = gameObject.tag;
                arrowScript.damage = damage;
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
