using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProyectile : Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;


    [Header("Audios")]
    [SerializeField] private AudioClip shootAudio;
    [SerializeField] private AudioClip ExplodeAudio;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

    }    

    public void ActivateProjectile()
    {
        SoundsManager.instance.PlaySound(shootAudio);
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.layer == Layers.PLAYER || collision.gameObject.layer == Layers.GROUND || collision.gameObject.layer == Layers.ATTACKZONE)
        {
            hit = true;
            base.OnTriggerEnter2D(collision);
            coll.enabled = false;
            SoundsManager.instance.PlaySound(ExplodeAudio);
            if (anim != null)
                anim.SetTrigger("Explode");
            else
                gameObject.SetActive(false);
        }        
    }
    private void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
