using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject projectilePowerUp;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float basefiringRate = .2f;
    [SerializeField] int powerUpActiveTime;
    [SerializeField] bool powerUpActive = false;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0;
    [SerializeField] float minFiringRate = .1f;

    Coroutine firingCoroutine;

    [HideInInspector] public bool isFiring;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();

    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            if(powerUpActive)
            {
                StopCoroutine(FireContinuously1());
                firingCoroutine = StartCoroutine(FireContinuously2());
            } else 
            {
                StopCoroutine(FireContinuously2());
                firingCoroutine = StartCoroutine(FireContinuously1());
            }
            
        } else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null; 
        }
        
    }

    IEnumerator FireContinuously1()
    {
        while(true)
        {
            
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();  
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed; 
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
                                                      basefiringRate + firingRateVariance);

            Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    IEnumerator FireContinuously2()
    {
        while (true)
        {

            GameObject instance = Instantiate(projectilePowerUp, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
                                                      basefiringRate + firingRateVariance);

            Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Powerup")
        {
            StartCoroutine(WaitToDeactivate());
            Destroy(other.gameObject);
        }
    }

    IEnumerator WaitToDeactivate()
    {
        powerUpActive = true;
        yield return new WaitForSeconds(powerUpActiveTime);
        powerUpActive = false;
    }


}
