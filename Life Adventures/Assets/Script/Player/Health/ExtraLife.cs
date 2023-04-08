using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("Audios y particulas")]
    [SerializeField] private AudioClip lifeGettedAudio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            SoundsManager.instance.PlaySound(lifeGettedAudio);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }   
    }
}
