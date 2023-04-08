using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public int score;
    [SerializeField] Text scoreText;
    
    private void Awake()
    {        
        if(SceneManager.GetActiveScene().name == "Nivel1")        
            score = 0;        
        else        
            score = PlayerPrefs.GetInt("Score");        
    }

    private void Update()
    {        
        scoreText.text = "Score: " + score;
    }
    public void SetScore(int scoreValue)
    {
        score += scoreValue;
    }


    public void saveRecord()
    {                
        int aux;
        for (int i = 0; i <= 10; i++)
        {
            if (score > PlayerPrefs.GetInt("Puntuacion" + i))
            {
                aux = PlayerPrefs.GetInt("Puntuacion" + i);
                PlayerPrefs.SetInt("Puntuacion" + i, score);
                score = aux;                
            }
        }
        PlayerPrefs.SetInt("Score", 0);
    }
}
