using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] GameObject bulletPowerUp;
    [SerializeField] int pickupEnemiesMin = 3;
    [SerializeField] int pickupEnemiesMax = 9;
    [SerializeField] float powerupLifeTime = 5f;
    [SerializeField] float powerupSpeed = 5f;

    private int pickupEnemiesLeft;
    private Transform enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        pickupEnemiesLeft = Random.Range(pickupEnemiesMin, pickupEnemiesMax + 1);
        enemyParent = GameObject.Find("Enemy").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EnemyDestroyedPowerUp ( Transform enemyTransform)
    {
        pickupEnemiesLeft--;
        if(pickupEnemiesLeft == 0)
        {
            GameObject instance = Instantiate(bulletPowerUp, enemyTransform.position, Quaternion.identity, enemyParent);
            pickupEnemiesLeft = Random.Range(pickupEnemiesMin, pickupEnemiesMax + 1);


            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.up * -powerupSpeed;
            }

            Destroy(instance, powerupLifeTime);
        }
    }
}
