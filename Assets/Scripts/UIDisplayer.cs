using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplayer : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider1;
    [SerializeField] Slider healthSlider2;
    [SerializeField] Health playerHealth1;
    [SerializeField] Health playerHealth2;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }


    // Start is called before the first frame update
    void Start()
    {
        healthSlider1.maxValue = playerHealth1.GetHealth();
        healthSlider2.maxValue = playerHealth2.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider1.value = playerHealth1.GetHealth();
        healthSlider2.value = playerHealth2.GetHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");
    }
}
