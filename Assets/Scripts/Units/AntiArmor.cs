using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiArmor : MonoBehaviour, IDamageable
{
    public float damage = 40;
    public float maxHealth = 100;
    public float MaxHealth => maxHealth;
    public Rigidbody2D AntiArmorRb;
    public float speed;
    public float cooldown = 2.5f;


    public int cost = 200;
    public float damageInterval = 2.0f;

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
            HealthBar healthBar = target.GetComponent<HealthBar>();
            if (healthBar != null)
            {
                healthBar.UpdateHealth(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnDestroy()
    {
        if (CompareTag("Player1"))
        {
            IAStats.AddMoney(220);
            IAStats.AddExp(240);
        }
        else if (CompareTag("Player2"))
        {
            PlayerStats.AddMoney(220);
            PlayerStats.AddExp(240);
        }
    }

    void FixedUpdate()
    {
        if (CompareTag("Player1"))
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            AntiArmorRb.velocity = Vector2.right * speed;
        }
        else if (CompareTag("Player2"))
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            AntiArmorRb.velocity = Vector2.left * speed;
        }
    }
}
