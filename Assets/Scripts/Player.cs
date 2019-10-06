using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{   
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathkick = new Vector2(25f, 25f);
    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    [SerializeField] BoxCollider2D myFeet;
    EnemyMovement eM;
    float gravityScaleAtStart;
    [SerializeField] Joystick joystick;


    //Message then methods
    void Start()
    {
        eM = FindObjectOfType<EnemyMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        joystick = FindObjectOfType<FloatingJoystick>();

        if (!isAlive)
        {
            return;
        }
        Run();
        ClimbLadder();        
        //Jump();
        FlipSprite();
        Die();

    }

    private void Run()
    {
       
            float controlThrow = joystick.Horizontal;
            //float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHorizontalSpeed);
        
    }

    private void ClimbLadder()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;  
            return;
        }
        float controlThrow = joystick.Vertical;
        //float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }
    public void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
            //return;
        }

        /*if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
       }*/
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies") { 

            Vector2 jumpVelocityToAdd = new Vector2(0f, 10f);
            myRigidbody.velocity += jumpVelocityToAdd;
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            GetComponent<Rigidbody2D>().velocity = deathkick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

   
    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * 3, 3f);
        }
    }
}
