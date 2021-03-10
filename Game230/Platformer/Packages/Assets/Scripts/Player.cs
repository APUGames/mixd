using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D playerCharacter;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathSeq = new Vector2(25f, 50f);
    float gravityScaleAtStart;

    bool isAlive = true; 

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAlive)
        {
            //return halts the meathod
            return;
        }
        Run();
        FlipSprite();
        Jump();
        Climb();
        Die();
         
    }

    private void Run()
    {
        //Horizontal movement value between -1 and 1
        float hMovement = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(hMovement * runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVelocity;
        //turn on run animation
        playerAnimator.SetBool("run", true);
        //turn off run animation
        bool hSpeed = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("run", hSpeed);
    }

    private void FlipSprite()
    {
        //if player is moving 
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;
        if (hMovement)
        {
            //reverse the current scaling of the x axis
            transform.localScale = new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1f);
        }

    }

    private void Jump()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("foreground"))) 
        {
            //will stop this function if false
            return;
        }


        if (Input.GetButtonDown("Jump"))
        {
            //get new y velocity based on a controlable variable
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;

        }
    
    }

    private void Climb()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            playerAnimator.SetBool("Climb",false);
            playerCharacter.gravityScale = gravityScaleAtStart; 

            return;
        }
        // "Vertical" From input axes
        float vMovement = Input.GetAxis("Vertical");
        // X needs to remain the same
        Vector2 climbVelocity = new Vector2(playerCharacter.velocity.x, vMovement * climbSpeed);
        playerCharacter.velocity = climbVelocity;

        playerCharacter.gravityScale = 0.0f;

        bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("Climb", vSpeed);


    }

    private void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("die");
            GetComponent<Rigidbody2D>().velocity = deathSeq;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
