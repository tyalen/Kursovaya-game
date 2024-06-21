using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Animator m_Animator;
    [SerializeField] private Rigidbody m_Rigidbody;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool onGround = true;
    [SerializeField] private float groundLength = 0.05f;
    [SerializeField] private Vector3 colliderOffset;
    [SerializeField] private float colliderRadius = 0.5f;
    bool isJumping = false;

    [Header("Movement")]
    [SerializeField] private float turnSpeed = 20f;
    [SerializeField] private float speedMultiplier = 3f;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        CheckGround();

        if (onGround)
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
        m_Animator.SetBool("IsJumping", isJumping);
    }



    void FixedUpdate()
    {
        //Импуты
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //чтобы не идти быстрее по диагонали
        m_Movement.Set(horizontalInput, 0f, verticalInput);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speedMultiplier * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            isJumping = true;
            m_Animator.SetBool("IsJumping", isJumping);
            Jumping();
        }
    }

   /* void Walking()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speedMultiplier * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }*/

    void Jumping()
    {
        m_Rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        onGround = false;
    }

    void CheckGround()
    {
        onGround = Physics.Raycast(transform.position + colliderOffset, Vector3.down, groundLength, groundLayer) || Physics.Raycast(transform.position - colliderOffset, Vector3.down, groundLength, groundLayer);
        //onGround = Physics.OverlapSphereNonAlloc(transform.position + colliderOffset, colliderRadius, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawWireSphere(transform.position + colliderOffset, colliderRadius);
        //Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
        //Gizmos.DrawWireSphere(transform.position + new Vector3(0, _grounderOffset), _grounderRadius);
    }
}
