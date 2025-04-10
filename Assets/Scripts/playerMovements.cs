using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    private CapsuleCollider2D myCapCollider2D;
    private Animator myAnimator;

    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private AudioSource meowSound;
    [SerializeField] private Transform hurtBox;
    [SerializeField] private float attackRadius = 1f;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCapCollider2D = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        Jump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Meow();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
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

    public void Meow()
    {
        if (meowSound != null)
        {
            meowSound.Play();
        }
    }

    public void Attack()
    {
        myAnimator.SetTrigger("Attacking");

        Collider2D[] leversToHit = Physics2D.OverlapCircleAll(hurtBox.position, attackRadius, LayerMask.GetMask("Lever"));

        foreach (Collider2D lever in leversToHit)
        {
            Lever leverScript = lever.GetComponent<Lever>();
            if (leverScript != null)
            {
                leverScript.FlipSprite();
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (hurtBox != null)
        {
            Gizmos.DrawWireSphere(hurtBox.position, attackRadius);
        }
    }
}
