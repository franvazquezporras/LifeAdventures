using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiPlataform : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D cPlayer;
    private BoxCollider2D cPlataform;
    private Bounds cPlataformBounds;
    //private Color colorgGizmos = Color.red;
    private float topPlataform, feetPlayer;
    public static bool getDown = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Feet");
        cPlayer = player.GetComponent<BoxCollider2D>();
        cPlataform = GetComponent<BoxCollider2D>();
        cPlataformBounds = cPlataform.bounds;
        topPlataform = cPlataformBounds.center.y + cPlataformBounds.extents.y;
    }

    private void Update()
    {
        feetPlayer = player.transform.position.y - cPlayer.size.y / 2;
        if (feetPlayer >= topPlataform)
        {
            cPlataform.isTrigger = false;
            gameObject.tag = "Ground";
            gameObject.layer = LayerMask.NameToLayer("PLATAFORM");
            //colorgGizmos = Color.green;
        }
        if ((!cPlataform.isTrigger && (feetPlayer < topPlataform - 0.1)) || getDown)
        {
            cPlataform.isTrigger = true;
            gameObject.tag = "Untagged";
            gameObject.layer = LayerMask.NameToLayer("Default");
            //colorgGizmos = Color.red;
            new WaitForSeconds(1f);
            
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = colorgGizmos;
    //    Gizmos.DrawSphere(cPlataform.transform.position, 0.25f);
    //}
}
