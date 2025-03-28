using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerMovements : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    float startingGravityScale;
    bool isHurting = false;
    [SerializeField] float runspeed = 10f;
    [SerializeField] float jumpspeed = 15f;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();

        startingGravityScale = myRigidBody2D.gravityScale;
    }

    void Update()
    {
       if (!isHurting)
        {
            Run();
            Jump();


        }

    }
    private void Jump()
    {
        if(!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;  }

        bool isJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (isJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody2D.linearVelocity.x, jumpspeed);
            myRigidBody2D.linearVelocity = jumpVelocity;
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runspeed, myRigidBody2D.linearVelocity.y);
        myRigidBody2D.linearVelocity = playerVelocity;

        FlipSprite();
        
    }

    private void FlipSprite()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody2D.linearVelocity.x) > Mathf.Epsilon;

        if(runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.linearVelocity.x), 1f);
        }
    }

}