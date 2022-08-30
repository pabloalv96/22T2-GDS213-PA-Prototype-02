using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{

    public GameObject shield;
    [SerializeField] int shieldCounter = 5;
    [SerializeField] int shieldCounterMax = 5;
    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shield.activeInHierarchy)
        {
            shieldCounter--;
            if (shieldCounter <= 0)
            {
                shield.SetActive(false);
                shieldCounter = shieldCounterMax;
            }
        } 

        if(other.gameObject.tag == "Shieldup")
        {
            shield.SetActive(true);
            Destroy(other.gameObject);
        }

    }

}
