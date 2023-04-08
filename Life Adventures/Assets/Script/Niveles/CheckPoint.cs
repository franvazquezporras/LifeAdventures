using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPoint : MonoBehaviour
{
    [SerializeField] ScoreControl score;
    [SerializeField] SceneController scene;
    [SerializeField] TimerController time;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
        {
            SaveScore();   
            scene.NextLevel();
        }
    }


    private void SaveScore()
    {
        int timeScore = Convert.ToInt32(time.getResTime() * 10);
        score.SetScore(timeScore);
        PlayerPrefs.SetInt("Score", score.score);        
        if(SceneManager.GetActiveScene().name == "Nivel3")
        {            
            score.saveRecord();
        }
    }
}
