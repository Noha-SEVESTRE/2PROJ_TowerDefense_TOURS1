using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour, IDamageable
{
    public float damage = 25;
    public float maxHealth = 200;
    public float MaxHealth => maxHealth;
    public Rigidbody2D MeleeRb;
    public float speed;
    public float cooldown = 1f;
    

    public int cost = 100;
    public float damageInterval = 1.0f;

    private Coroutine damageCoroutine;
    private GameObject currentTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageCoroutine == null && collision.gameObject != currentTarget && collision.gameObject.CompareTag(gameObject.tag) != true)
        {
            StartDamaging(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != currentTarget && collision.gameObject.CompareTag(gameObject.tag) != true)
        {
            StopDamaging();
            StartDamaging(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == currentTarget)
        {
            StopDamaging();
        }
    }

    private void StartDamaging(GameObject target)
    {
        currentTarget = target;
        damageCoroutine = StartCoroutine(DealDamagePeriodically(currentTarget));
    }

    private void StopDamaging()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DealDamagePeriodically(GameObject target)
    {
        while (true) 
        {
            if (target != null) 
            {
                HealthBar healthBar = target.GetComponent<HealthBar>();
                if (healthBar != null)
                {
                    healthBar.UpdateHealth(damage);
                }
            }
            else
            {
                StopDamaging();
                yield break; 
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

     private void OnDestroy()
    {
        if (CompareTag("Player1"))
        {
            IAStats.AddMoney(120);
            IAStats.AddExp(140);
        }
        else if (CompareTag("Player2"))
        {
            PlayerStats.AddMoney(120);
            PlayerStats.AddExp(140);
        }
    }

    void FixedUpdate()
    {
        if (CompareTag("Player1"))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            MeleeRb.velocity = Vector2.right * speed;
        }
        else if (CompareTag("Player2"))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            MeleeRb.velocity = Vector2.left * speed;
        }
    }
}
