using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace PabloAS
{
    public class DestroyedEnemyListener : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        [SerializeField] int enemyDestroyedCounter = 0;

        [SerializeField] private TextMeshProUGUI shieldCounterText;
        [SerializeField] int shieldDroppedCounter = 0;
        private void OnEnable()
        {
            Health.OnDestroyedEnemy += IncreaseEnemyCounter;
        }

        private void OnDisable()
        {
            Health.OnDestroyedEnemy -= IncreaseEnemyCounter;
        }

        private void IncreaseEnemyCounter ()
        {
            enemyDestroyedCounter++;
            enemyCounterText.text = "Enemies: " + enemyDestroyedCounter.ToString();
        }
        
        private void IncreaseShieldCounter ()
        {
            shieldDroppedCounter++;
            shieldCounterText.text = "Shields Grabbed: " + shieldDroppedCounter.ToString();
        }
    }
}


