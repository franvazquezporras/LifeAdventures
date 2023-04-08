using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InestablePlataformController : MonoBehaviour
{   
    private Rigidbody2D rb2b;    
    private Vector3 iniPosition;
    //Caida efecto
    private float delayCaida = 0.5f;
    private bool tremble;
    private float trembleRight = 0.02f;
    private float margin = 0.04f;

    //Suavizar Respawn
    [SerializeField] private float delayRespawn = 5f;
    [SerializeField] private GameObject[] sprites;
    private SpriteRenderer[] sprSprites = new SpriteRenderer[3];


    void Awake()
    {
        rb2b = GetComponent<Rigidbody2D>();        
        iniPosition = transform.position;        
        for(int i = 0; i < sprites.Length; i++)
        {
            sprSprites[i] = sprites[i].GetComponent<SpriteRenderer>();
        }        
    }

    private void Update()
    {
        if (tremble)
        {
            transform.position = new Vector3(transform.position.x + trembleRight, transform.position.y, transform.position.z);
            if (transform.position.x >= iniPosition.x + margin || transform.position.x <= iniPosition.x - margin)
                trembleRight *= -1;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //transforma la fisica
        if (col.gameObject.layer == Layers.PLAYER)
        {
            Invoke("Caida", delayCaida);
            Invoke("Respawn", delayCaida + delayRespawn);
            tremble = true;
        }
        if (col.gameObject.layer != Layers.PLAYER && col.gameObject.layer != Layers.FEETS)
            gameObject.GetComponent<Collider2D>().enabled = false;
    }

    void Caida()
    {
        rb2b.isKinematic = false;        
    }

    void Respawn()
    {
        transform.position = iniPosition;
        rb2b.isKinematic = true;
        rb2b.velocity = Vector3.zero;
        tremble = false;
        //Suavizar reaparicion
        for (int i = 0; i < sprSprites.Length; i++)        
            RespawnAlpha(sprSprites[i], 0f);
        StartCoroutine("ActiveInfo");    
    }

    IEnumerator ActiveInfo()
    {
        for (float i = 0.0f; i <= 1; i += 0.1f)
        {
            foreach(SpriteRenderer spr in sprSprites)
                RespawnAlpha(spr, i);
            yield return new WaitForSeconds(0.05f);
            
        }
        foreach (SpriteRenderer spr in sprSprites)
            RespawnAlpha(spr, 1);


    }
    private void RespawnAlpha(SpriteRenderer spr, float alpha)
    {
        Color color = spr.material.color;
        color.a = alpha;
        spr.material.color = color;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
