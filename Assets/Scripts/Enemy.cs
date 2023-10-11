using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed = 10f;

    public float startHealth = 100;

    public bool isFlying = false;

    [Header("Burn Stuff")]
    private bool isBurned = false;
    private float burnTimer = 0f;
    private float burnParticleTimer = 0f;
    public GameObject burnEffect;
    public int burnDamage = 0;
    [Header("Poison Stuff")]
    private bool isPoisoned = false;
    private float poisonTimer = 0f;
    private float poisonParticleTimer = 0f;
    public GameObject poisonEffect;
    public int poisonDamage = 2;

    [HideInInspector]
    private float health;

    public int worth = 5;

    public GameObject deathEffect;
    public Transform hitPoint;
    
    //[Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void BurnMob(float duration)
    {
        if (isBurned)
        {
            return;
        }
        burnParticleTimer = 1f;
        burnTimer = duration;
        isBurned = true;
    }

    public void PoisonMob(float duration)
    {
        if (isPoisoned)
        {
            return;
        }
        poisonParticleTimer = 1f;
        poisonTimer = duration;
        isPoisoned = true;
    }

    public void Update()
    {
        if (isBurned)
        {
            burnParticleTimer -= Time.deltaTime;
            burnTimer = Mathf.Max(0, burnTimer - Time.deltaTime);
            if (burnParticleTimer < 0)
            {
                TakeDamage(burnDamage);
                GameObject effect = Instantiate(burnEffect, hitPoint.position, Quaternion.identity);
                Destroy(effect, 5f);
                burnParticleTimer = 1f;
            }
            if (burnTimer == 0)
            {
                isBurned = false;
            }
        }
        if (isPoisoned)
        {
            poisonParticleTimer -= Time.deltaTime;
            poisonTimer = Mathf.Max(0, poisonTimer - Time.deltaTime);
            if (poisonParticleTimer < 0)
            {
                TakeDamage(poisonDamage);
                GameObject effect = Instantiate(poisonEffect, hitPoint.position, Quaternion.identity);
                Destroy(effect, 5f);
                poisonParticleTimer = 1f;
            }
            if (poisonTimer == 0)
            {
                isPoisoned = false;
            }
        }
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow (float percent)
    {
        speed = startSpeed * (1f - percent);
    }

    void Die()
    {
        isDead = true;

        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        GameObject effect = Instantiate(deathEffect, hitPoint.position, Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money += worth;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }
}
