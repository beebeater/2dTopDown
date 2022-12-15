using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;

    [SerializeField] Text scoreText;

    float currentScore;

    float score = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        currentScore = score += 1 * Time.deltaTime;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
