using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpell : MonoBehaviour
{
    float moveSpeed;
    Rigidbody2D rb2d;
    Vector2 direction;
    private GameObject player;
    private Animator anim;
    [Header("Audios")]
    [SerializeField] private AudioClip shootAudio;
    [SerializeField] private AudioClip ExplodeAudio;
    private void Awake()
    {
        moveSpeed = 5;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SoundsManager.instance.PlaySound(shootAudio);
        direction = (player.transform.position - transform.position).normalized * moveSpeed;
        rb2d.velocity = new Vector2(direction.x, direction.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER || collision.gameObject.layer == Layers.GROUND)
        {
            rb2d.velocity = Vector2.zero;
            SoundsManager.instance.PlaySound(ExplodeAudio);
            if (anim != null)
                anim.SetTrigger("Explode");
            if (collision.gameObject.layer == Layers.PLAYER)
                player.GetComponent<Health>().TakeDamage(1);
        }
    }
    private void Desactivate()
    {
        Destroy(gameObject);
    }
}
