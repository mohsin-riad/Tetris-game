using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public float regularScore;
    public float highScore;
    public Text regularScoreText;
    public Text highScoreText;
    public void addScore(){
        regularScore++;
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetFloat("Highscore");
    }

    // Update is called once per frame
    void Update()
    {
        regularScoreText.text = regularScore.ToString();
        highScoreText.text = highScore.ToString();
        if(regularScore > highScore){
            PlayerPrefs.SetFloat("Highscore", regularScore);
            highScore = regularScore;
        }
    }
    public void clearScore(){
        regularScore = 0;
    } 
}
