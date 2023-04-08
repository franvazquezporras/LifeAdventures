using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("Audios y particulas")]
    [SerializeField] private AudioClip coinGettedAudio;    
    private ParticleSystem particle;

    [SerializeField] private ScoreControl scores;
    private SpriteRenderer spr;
    private bool active = true;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER && active)
        {
            GameManager.getCoin();
            spr.enabled = false;
            SoundsManager.instance.PlaySound(coinGettedAudio);
            particle.Play();
            scores.SetScore(10);
            active = false;      
            
        }
    }
}
