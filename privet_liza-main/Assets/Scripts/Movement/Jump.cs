using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PhysicsMovement horizontalSpeed;

    private ObstacleClimb isGoingToClimb;
    [SerializeField] private float groundedOffset = 0.08f;
    [SerializeField] private float jumpDelay = 0.2f;
    [SerializeField] private float groundedRadius = 0.15f;

    [SerializeField] private float fallGravityScale = -40f;
    [SerializeField] private float gravityScale = 10f;

    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private bool onGround = true;
    [SerializeField] private bool isFalling;

    public bool ledgeDetected;

    [SerializeField] private LayerMask groundLayer;

    private readonly Collider[] groundCollider = new Collider[1];

    private float jumpForce;
    public bool isJumping = false;
    float speedTemp;

    void Start()
    {
        animator = GetComponent<Animator>();
        isGoingToClimb = GetComponent<ObstacleClimb>();
        rb = GetComponent<Rigidbody>();
        horizontalSpeed = GetComponent<PhysicsMovement>();
        speedTemp = horizontalSpeed.speed;
    }

    private void FixedUpdate()
    {
        CheckGround();
        FallGravity();
    }

    void OnAnimatorMove()
    {
        animator.SetBool("IsJumping", isJumping);
        MakeJumpWithDelay();
    }

    void MakeJumpWithDelay()
    {
        if (Input.GetButtonDown("Jump") && onGround && !isJumping && isGoingToClimb.isClimbing == false)
        {
            isJumping = true;
            StartCoroutine(MakeJump());
        }
    }

    IEnumerator MakeJump()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpDelay);
        float gravityY = Physics.gravity.y;
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (gravityY + rb.drag));
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    void FallGravity()
    {
        if (rb.velocity.y <= 0 && !onGround)
        {
            isFalling = true;
            rb.drag = -fallGravityScale;
        }
        else
        {
            rb.drag = gravityScale;
            isFalling = false;
        }

    }
    void CheckGround()
    {
        var grounded = Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, groundedOffset), groundedRadius, groundCollider, groundLayer) > 0;
        if (!onGround && grounded)
        {
            onGround = true;
            isJumping = false;
            isFalling = false;
            horizontalSpeed.speed = speedTemp;
        }
        else if (onGround && !grounded)
        {
            onGround = false;
            horizontalSpeed.speed *= 0.4f;
        }
    }
        


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, groundedOffset), groundedRadius);
    }




}
