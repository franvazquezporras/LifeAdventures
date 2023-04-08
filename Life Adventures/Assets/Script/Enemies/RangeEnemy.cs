using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;   

    [Header("Range Attack")]
    [SerializeField] private Transform attackSpawn;
    [SerializeField] private GameObject[] attackProyectiles;

    [Header("Collider Parameters")]
    [SerializeField] private float colDistance;
    [SerializeField] private BoxCollider2D col;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;    
    private EnemyPatrol enemypatrol;
    BasicIA father;    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemypatrol = GetComponentInParent<EnemyPatrol>();
        father = GetComponent<BasicIA>();
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        PlayerDetection();        
    }

    private void PlayerDetection()
    {
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

    private void RangeAttack()
    {
        coolDownTimer = 0;
        attackProyectiles[FindProyectile()].transform.position = attackSpawn.position;
        attackProyectiles[FindProyectile()].GetComponent<EnemyProyectile>().ActivateProjectile();
    }
    private int FindProyectile()
    {
        for(int i = 0; i < attackProyectiles.Length; i++)
        {
            if (!attackProyectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private bool PlayerInZone()
    {        
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center + transform.right * range * transform.localScale.x * colDistance,
            new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z),
            0, Vector2.left, 0, playerLayer);            
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(col.bounds.center + transform.right * range * transform.localScale.x * colDistance, new Vector3(col.bounds.size.x * range, col.bounds.size.y, col.bounds.size.z));
    }

}
