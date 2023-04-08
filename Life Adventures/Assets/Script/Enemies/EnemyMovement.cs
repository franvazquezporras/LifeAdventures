using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] float speed;
    [SerializeField] LayerMask scenaryC;

    [Header("Fuerzas Salto")]
    [SerializeField] float jumpUp;
    [SerializeField] float shortJump;

    [Header("Modificadores velocidad")]
    [SerializeField] float modUp;
    [SerializeField] float modDown;
    [SerializeField] float modShort;

    [Header("Info")]
    //[SerializeField] bool floor;
    //[SerializeField] bool jumping;
    //[SerializeField] bool falling;
    

    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D col;
    [SerializeField] BasicIA father;

    float dirX = 1;
    float controlVelocity = 1;
    
    void Update()
    {

        if (!father.canAttack)
        {
            
            if (!father.floor)
                CheckFall();      
            if(father.canMove)
                Move();
            if (father.floor && !father.jump && !father.fall)
                Detections();
            CheckFloor();
        }
        else
        {
            rb2d.velocity = Vector3.zero;
        }
        
    }

    private void CheckFall()
    {
        if (rb2d.velocity.y < 0.0001f)
            father.fall = true;
        
    }
    private void CheckFloor()
    {
        Vector2 floorLeft = new Vector2(transform.position.x - (col.bounds.extents.x - 0.1f), transform.position.y - (col.bounds.extents.y));
        Vector2 floorRight = new Vector2(transform.position.x + (col.bounds.extents.x - 0.1f), transform.position.y - (col.bounds.extents.y));
        RaycastHit2D raycastFloorLeft = Physics2D.Raycast(floorLeft, Vector2.down, 0.025f, scenaryC);
        RaycastHit2D raycastFloorRight = Physics2D.Raycast(floorRight, Vector2.down, 0.025f, scenaryC);
        Debug.DrawRay(floorLeft, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(floorRight, Vector2.down * 0.1f, Color.red);
        if (raycastFloorLeft || raycastFloorRight)
        {
            if (father.fall)
            {
                father.jump = false;
                father.fall = false;
            }
            father.floor = true;            
            controlVelocity = 1;
        }  
        else
            father.floor = false;
    }
    private void Detections()
    {
        //Comprobar que no hay nada debajo
        if (!RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y - (col.bounds.extents.y), Vector2.down, 0.1f, Color.red))
            //Comprobar si es capaz de bajar
            if (RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y - (col.bounds.extents.y), Vector2.down, 1.1f, Color.cyan))
                controlVelocity = modDown;
            else
            {
                //Comprueba si hay suelo delante al que saltar
                if (RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y-1, Vector2.right * dirX, 1.1f, Color.black))
                {
                    //Comprueba que no haya muro en el sitio al que va saltar
                    if(!RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y, Vector2.right * dirX, 1.1f, Color.green))
                    {
                        controlVelocity = modShort;
                        Jump(shortJump);
                    }                    
                }
                //si no puede saltar gira
                else
                    Flip();
            }                
        else
        {
            //Comprueba si hay un muro deltante
            if (RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y, Vector2.right * dirX, 0.35f, Color.yellow))
            {
                //comprueba que el muro no supere su altura para subirlo
                if (!RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y + 1, Vector2.right * dirX, 1f, Color.blue))
                {
                    controlVelocity = modUp;
                    Jump(jumpUp);
                }
                //si el muro es demasiado alto gira
                else
                    Flip();
            }
            //si no hay muro delante ni nada debajo gira
            else if (RaycastLocate(transform.position.x + (col.bounds.extents.x * dirX), transform.position.y, Vector2.right * dirX, 0.05f, Color.white))
                Flip();
        }
    }
    

    private void Flip()
    {
        rb2d.velocity = Vector2.zero;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        dirX *= -1;
    }
    private void Move()
    {        
        rb2d.velocity = new Vector2(speed * controlVelocity * dirX, rb2d.velocity.y);
    }

    private void Jump(float force)
    {
        father.jump = true;
        Vector2 jumpForce = new Vector2(0, force);
        rb2d.AddForce(jumpForce, ForceMode2D.Impulse);

    }

    private bool RaycastLocate(float x, float y,Vector2 dir, float size,Color color)
    {
        Vector2 posRaycast = new Vector2(x, y);
        RaycastHit2D raycastItem = Physics2D.Raycast(posRaycast, dir, size, scenaryC);
        Debug.DrawRay(posRaycast, dir * size, color);
        return raycastItem;
    }
}
