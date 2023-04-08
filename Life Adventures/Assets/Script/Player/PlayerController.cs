using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Desplazamientos y saltos")]
    private float movementSpeed = 2;
    private float jumpForce = 5;    
    private float doubleJumpForce = 6f;
    public float movementX;
    private float movementY;
    private bool flipSprite = true;
    private bool doubleJump;
    private bool falling = false;
    
    [Header("Vida y daño")]
    public static bool inmune;
    
    public bool gettingDmg { get; set; }
    [SerializeField] Health hpdmg;
    
    [Header("Componentes")]    
    private Rigidbody2D rb2d;    
    private Animator anim;
    [SerializeField] private FeetControl feetCollision;

    [Header("Audios")]
    [SerializeField] private AudioClip jumpAudio;

    internal static PlayerController instance;

    private void Awake()
    {        
        gettingDmg = false;
        rb2d = GetComponent<Rigidbody2D>();        
        anim = GetComponent<Animator>();
        hpdmg = GetComponent<Health>();
    }
    
    private void FixedUpdate()
    {
        if (!gettingDmg && GameManager.gameOn) {
            Jump();
            Flip(movementX);
            UpdateLateralMovement();
            UpdateVerticalMovement();
        }    
    }

    private void Flip(float horizontal)
    {
        if((horizontal>0 && !flipSprite) || (horizontal<0 && flipSprite))
        {
            flipSprite = !flipSprite;
            Vector3 frontCol = transform.localScale;
            frontCol.x = frontCol.x * -1;
            transform.localScale = frontCol;
        }
    }
    
    private void UpdateLateralMovement()
    {        
        float speedX = 0;
        movementX = Input.GetAxis("Horizontal");
        bool walk = false, run = false;

        if (!((Input.GetKey("a") || Input.GetKey("left")) && (Input.GetKey("d") || Input.GetKey("right"))))
        {
            speedX += movementX * movementSpeed;
            if (speedX != 0 && Input.GetKey("left shift"))
            {
                speedX += (2 * Input.GetAxisRaw("Horizontal"));
                run = true;
            }
            else if (speedX != 0 && !Input.GetKey("left shift"))            
                walk = true;            
        }       
        anim.SetBool("Run", run);
        anim.SetBool("Walk", walk);
        rb2d.velocity = new Vector2(speedX, rb2d.velocity.y);        
    }

    private void UpdateVerticalMovement()
    {        
        float speedY = 0;
        movementY = Input.GetAxis("Vertical");
        bool climb = false;        
        if (movementY < 0 && !SemiPlataform.getDown)        
            SemiPlataform.getDown = true;
        if (movementY>=0 && SemiPlataform.getDown)
            SemiPlataform.getDown = false;
        
        if (feetCollision.ladder)
        {
                rb2d.gravityScale = 0;
                speedY += movementY * movementSpeed;
                climb = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, speedY);            
        }
        else
            rb2d.gravityScale = 1;
        anim.SetBool("Climb", climb);
    }

    void Jump()
    {
        if (feetCollision.floor)        
            doubleJump = true;
        if (Input.GetKeyDown("space") && doubleJump && !feetCollision.floor)
        {
            SoundsManager.instance.PlaySound(jumpAudio);
            anim.SetTrigger("DoubleJumpT");
            rb2d.velocity = new Vector2(rb2d.velocity.x, doubleJumpForce);
            doubleJump = false;
        }else if (Input.GetKey("space") && feetCollision.floor)
        {            
                SoundsManager.instance.PlaySound(jumpAudio);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        AnimarSalto();
    }
    public bool canAttack()
    {
        return feetCollision.floor;
    }
    void AnimarSalto()
    {

        if (feetCollision.floor == false && Input.GetKey("space"))
        {
            anim.SetBool("Jump", true);
            falling = true;
        }   
        else if (feetCollision.floor == false)
            anim.SetBool("Falling", true);
        else if (feetCollision.floor == true)
        {            
            anim.SetBool("Falling", false);
            anim.SetBool("Jump", false);
            if (falling)
            {
                gameObject.GetComponentInChildren<FeetControl>().jumpSmoke.Play();
                falling = false;
            }                
        }
        if (rb2d.velocity.y < 0 && feetCollision.floor == false)
            anim.SetBool("Falling", true);
        else if (rb2d.velocity.y > 0)
            anim.SetBool("Falling", false);
    }

    public void PlayerHit(float x)
    {
        inmune = true;
        float side = Mathf.Sign(x - transform.position.x);
        rb2d.AddForce(new Vector2(4 * -side, 2), ForceMode2D.Impulse);
        inmune = false;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.SPIKES && !inmune)
            PlayerHit(collision.transform.position.x);
        if (collision.gameObject.layer == Layers.ESTATUA)
            collision.gameObject.GetComponent<EstatuaControl>().activate = true;
        if ((collision.gameObject.layer == Layers.SPIKES || collision.gameObject.layer == Layers.ENEMY) && !inmune)
            PlayerHit(collision.transform.position.x);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Layers.FALLDEATH)
            hpdmg.TakeDamage(10);
    }

}
