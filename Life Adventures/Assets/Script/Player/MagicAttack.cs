using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    private float speed = 10;
    private float direction;
    private bool hit;
    private BoxCollider2D boxCol;
    private Animator anim;
    private float lifeTime;

    [Header("Audios")]
    [SerializeField] private AudioClip ExplodeAudio;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) 
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer != Layers.PLAYER && collision.gameObject.layer != Layers.FEETS)
        {
            hit = true;
            boxCol.enabled = false;
            anim.SetTrigger("Explode");
            SoundsManager.instance.PlaySound(ExplodeAudio);
            if (collision.gameObject.layer == Layers.ENEMY)
                collision.GetComponent<Health>().TakeDamage(1);
        }
        

    }

    public void setDirection(float dire)
    {
        lifeTime = 0;
        direction = dire;
        gameObject.SetActive(true);
        hit = false;
        boxCol.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dire)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
