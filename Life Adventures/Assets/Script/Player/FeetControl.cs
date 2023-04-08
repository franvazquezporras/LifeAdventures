using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetControl : MonoBehaviour
{
    public bool floor;
    public bool ladder;

    [Header("Particulas")]
    [SerializeField] private ParticleSystem feetSmoke;
    [SerializeField] public ParticleSystem jumpSmoke;
    private ParticleSystem.EmissionModule emisionFeetSmoke;
    


    private void Start()
    {
        emisionFeetSmoke = feetSmoke.emission;
        
    }
    private void Update()
    {
        CheckFeetSmoke();
    }

    private void CheckFeetSmoke()
    {
        if (floor && gameObject.GetComponentInParent<PlayerController>().movementX != 0)
            emisionFeetSmoke.rateOverTime = 50;
        else
            emisionFeetSmoke.rateOverTime = 0;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.layer == Layers.LADDER && gameObject.layer == Layers.FEETS)                   
            ladder = true;
    }  

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.LADDER && gameObject.layer == Layers.FEETS)                   
            ladder = false;            
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == Layers.GROUND && gameObject.layer == Layers.FEETS)
            floor = true;        
        if (col.gameObject.layer == Layers.PLATAFORM && gameObject.layer == Layers.FEETS)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);            
            GameObject.FindGameObjectWithTag("Player").transform.parent = col.transform;
            floor = true;
        }        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == Layers.GROUND && gameObject.layer == Layers.FEETS)
            floor = false;
        if (col.gameObject.layer == Layers.PLATAFORM && gameObject.layer == Layers.FEETS)
        {
            GameObject.FindGameObjectWithTag("Player").transform.parent = null;
            floor = false;
        }
    }

    
}
