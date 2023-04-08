using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicIA : MonoBehaviour
{
    public bool canAttack = false;
    public bool canMove;
    public bool floor;
    public bool jump;
    public bool fall;
    public bool hit;
    [SerializeField] private int scoreEnemy;
    [SerializeField] private ScoreControl scores;


    private void OnDisable()
    {
        scores.SetScore(scoreEnemy);        
    }
}
