using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private int min;
    [SerializeField] private int seg;
    [SerializeField] Text timer;
    private float restTime;
    private bool running;
    Health player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        restTime = (min * 60) + seg;
        running = true;
    }

    private void Update()
    {
        if (running)
        {
            restTime -= Time.deltaTime;
            if (restTime < 1)
            {
                running = false;
                player.TakeDamage(10);
                
            }
            int timM = Mathf.FloorToInt(restTime / 60);
            int timS = Mathf.FloorToInt(restTime % 60);
            timer.text = string.Format("{00:00}:{01:00}", timM, timS);
        }
    }
    public float getResTime()
    {
        return restTime;
    }
}
