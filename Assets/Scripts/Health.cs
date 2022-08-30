//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] bool applyCameraShake;

    AudioPlayer audioPlayer;

    ScenesLoader sceneLoader;

    ScoreKeeper scoreKeeper;

    //PowerUpSpawn powerUpSpawn;

    ShieldSpawner shieldSpawn;

    Shields shields;

    CameraShake cameraShake;

    [Header("IFrame")]
    [SerializeField] Color flashColor;
    [SerializeField] Color originalColor;
    [SerializeField] float flashDuration;
    [SerializeField] int numberOfFlashes;
    [SerializeField] Collider2D triggerCollider;
    [SerializeField] SpriteRenderer mySprite;

    public delegate void DestroyedEnemyAction();
    public static event DestroyedEnemyAction OnDestroyedEnemy;



    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sceneLoader = FindObjectOfType<ScenesLoader>();
        //powerUpSpawn = FindObjectOfType<PowerUpSpawn>();
        shieldSpawn = FindObjectOfType<ShieldSpawner>();
        shields = FindObjectOfType<Shields>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            if(!isPlayer)
            {
                TakeDamage(damageDealer.GetDamage());
            }

            if(isPlayer)
            {
                if(!shields.shield.activeInHierarchy)
                {
                    TakeDamage(damageDealer.GetDamage());
                    StartCoroutine(FlashCo());
                }
            }

            ShakeCamera();
            PlayHitEffect();
            audioPlayer.PlayExplosionClip();
            damageDealer.Hit();
        }

    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if(health <=0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            shieldSpawn.EnemyDestroyedShieldUp(transform);
            //powerUpSpawn.EnemyDestroyedPowerUp(transform);
            scoreKeeper.ModifyScore(score);

            if (OnDestroyedEnemy != null)
            {
                OnDestroyedEnemy();
            }

        } else
        {
            sceneLoader.GameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    IEnumerator FlashCo ()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp<numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

}
