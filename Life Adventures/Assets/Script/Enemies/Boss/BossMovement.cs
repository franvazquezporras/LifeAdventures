using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;
    private float coolDownShoot = 3;
    private float coolDownTeleport = 10;
    private float coolDownTp;
    private float coolDownSt;
    [SerializeField] GameObject spellBoss;
    private GameObject player;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        Teleport();
        coolDownSt = coolDownShoot;
        coolDownTp = coolDownTeleport;
    }

    private void Update()
    {
        CoolDowns();
        FlipBoss();
    }

    public void CoolDowns()
    {
        coolDownSt -= Time.deltaTime;
        coolDownTp -= Time.deltaTime;
        if (coolDownSt <= 0f)
        {
            ShootPlayer();
            coolDownSt = coolDownShoot;

        }
        if (coolDownTp <= 0f)
        {
            coolDownTp = coolDownTeleport;
            Teleport();
        }
    }
    public void ShootPlayer()
    {
        anim.SetTrigger("Attack");
        GameObject spell = Instantiate(spellBoss, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == Layers.PLAYER)
        {
            collision.GetComponent<Health>().TakeDamage(2);
        }
    }

    public void Teleport()
    {
        int initPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initPosition].position;
    }

    public void FlipBoss()
    {
        if(transform.position.x > player.transform.position.x)        
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

    }
}
