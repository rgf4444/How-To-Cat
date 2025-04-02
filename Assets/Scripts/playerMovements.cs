using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    private CapsuleCollider2D myCapCollider2D;

    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCapCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        Jump();
    }

    private void Jump()
    {
        if (!myCapCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (Input.GetButtonDown("Jump"))
        {
            myRigidBody2D.linearVelocity = new Vector2(myRigidBody2D.linearVelocity.x, jumpSpeed);
        }
    }

    private void Run()
    {
        float controlThrow = Input.GetAxisRaw("Horizontal"); // Snappier movement

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.linearVelocity.y);
        myRigidBody2D.linearVelocity = playerVelocity;

        FlipSprite();
    }

    private void FlipSprite()
    {
        bool runningHorizontally = Mathf.Abs(myRigidBody2D.linearVelocity.x) > Mathf.Epsilon;

        if (runningHorizontally)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.linearVelocity.x), 1f);
        }
    }
}
