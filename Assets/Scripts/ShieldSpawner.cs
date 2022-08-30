using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField] GameObject shieldPowerUp;
    [SerializeField] int pickupEnemiesMin = 2;
    [SerializeField] int pickupEnemiesMax = 8;
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

    public void EnemyDestroyedShieldUp(Transform enemyTransform)
    {
        pickupEnemiesLeft--;
        if (pickupEnemiesLeft == 0)
        {
            GameObject instance = Instantiate(shieldPowerUp, enemyTransform.position, Quaternion.identity, enemyParent);
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
