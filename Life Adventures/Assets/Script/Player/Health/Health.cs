using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    private float limitHealth = 10;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Iframe")]
    [SerializeField] private float duration;
    [SerializeField] private int numberFlash;
    private SpriteRenderer spr;

    [Header("Componets")]
    [SerializeField] private Behaviour[] components;

    [Header("Audio")]
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip deathAudio;

    

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);
        if (currentHealth > 0)
        {
            SoundsManager.instance.PlaySound(hitAudio);
            anim.SetTrigger("Hit");
            StartCoroutine("Invunerability");
        }
        else
        {      
            if (!dead)
            {
                SoundsManager.instance.PlaySound(deathAudio);
                anim.SetTrigger("Death");
                if (gameObject.layer == Layers.PLAYER)
                    StartCoroutine("PlayerRespawn");
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
            }            
        }
    }

    public void AddHealth(float healthValue)
    {
        if(currentHealth == maxHealth)
        {
            if (maxHealth < limitHealth)
                maxHealth++;
        }
        currentHealth = Mathf.Clamp(currentHealth + healthValue, 0, maxHealth);
        
        
    }

  
    IEnumerator PlayerRespawn()
    {
        
        GameManager.gameOn = false;        
        yield return new WaitForSeconds(1f);        
        GameManager.deathPlayer = true;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(12,14,true);
        for(int i = 0; i < numberFlash; i++)
        {
            spr.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(duration / (numberFlash * 2));
            spr.color = Color.white;
            yield return new WaitForSeconds(duration / (numberFlash * 2));
        }

        Physics2D.IgnoreLayerCollision(12, 14, false);
    }


    private void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
