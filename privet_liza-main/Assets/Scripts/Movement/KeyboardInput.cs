using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private PhysicsMovement movement;
    [SerializeField] private Animator animator;

    [SerializeField] private float grabDistance = 2f; // ����������, �� ������� �������� ����� ��������� �� ������
    [SerializeField] private LayerMask grabbableLayer; // ����, ������� ����� �������
    [SerializeField] private Transform holdPoint; // �����, � ������� �������� ������ ������

    [SerializeField] private float turnSpeed = 50f;
    Quaternion rotation = Quaternion.identity;

    private bool isCarrying = false; // ����, �����������, ����� �� �������� ������
    private GameObject carriedObject; // ������ �� ����������� ������


    Jump isJumping;

    bool hasHorizontalInput;
    bool hasVerticalInput;
    bool isWalking;

    Vector3 movementVector;

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementVector.Set(-verticalInput, 0f, horizontalInput);
        movementVector.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementVector, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);

        movement.Move(new Vector3(-verticalInput, 0, horizontalInput));
        movement.Rotation(rotation);

        if (!isCarrying)
        {
            movement.Move(movementVector);
        }
        else if (carriedObject != null)
        {
            movement.Move(movementVector); // �������� ��� ��� ����� ���������
            // ���������� ������ � ����� ���������
            carriedObject.transform.position = holdPoint.position;
        }

        //movement.CheckObstacle();

        hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("IsWalking", isWalking);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarrying)
            {
                // ���������, ���� �� ������ ����� � ����������, ������� ����� �������
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, grabbableLayer))
                {
                    carriedObject = hit.collider.gameObject;
                    isCarrying = true;
                    Rigidbody rb = carriedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    carriedObject.transform.SetParent(holdPoint);
                    carriedObject.transform.localPosition = Vector3.zero;
                    // ����� ����� �������� �������� �������� �������
                }
            }
            else
            {
                // ��������� ������
                isCarrying = false;
                Rigidbody rb = carriedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                carriedObject.transform.SetParent(null);
                carriedObject = null;
                // ����� ����� �������� �������� ���������� �������
            }
        }
    }

}
