using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

int score;
int highScore;
[SerializeField] TextMeshProUGUI scoreText;
[SerializeField] TextMeshProUGUI highScoreText;
    public void Start () {
        score = PlayerPrefs.GetInt("score");
        highScore = PlayerPrefs.GetInt("highscore");
        if (score > highScore) {
            PlayerPrefs.SetInt("highscore", score);
        }
        scoreText.text = scoreText.text + score.ToString();
        highScoreText.text = highScoreText.text + highScore.ToString();
    }
}
