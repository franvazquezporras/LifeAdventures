using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header ("Collider Parameters")]
    [SerializeField] private float colDistance;
    [SerializeField] private BoxCollider2D col;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemypatrol;
    BasicIA father;

    [Header("Audios")]
    [SerializeField] private AudioClip swordAudio;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<EnemyPatrol>();
        father = GetComponent<BasicIA>();
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;
       
        if (PlayerInZone())
        {
            father.canMove = false;
            anim.SetBool("waiting", true);
            father.canAttack = true;
            if (coolDownTimer >= attackCooldown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("Attack");
                
            }
        }
        else
        {
            father.canMove = true;
            father.canAttack = false;
            anim.SetBool("waiting", false);

        }
        if (enemypatrol != null)
            enemypatrol.enabled = !PlayerInZone();
    }

    private bool PlayerInZone()
    {
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center+transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        if(hit.collider != null)        
            playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center+transform.right*range*transform.localScale.x * colDistance, new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInZone())
        {
            SoundsManager.instance.PlaySound(swordAudio);
            playerHealth.TakeDamage(damage);
        }
    }
}
