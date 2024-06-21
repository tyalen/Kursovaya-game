using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    [SerializeField] private float grabDistance = 2f;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private float raycastOffset = 0.5f;

    private Rigidbody grabbedObject = null;
    private ConfigurableJoint joint;
    public bool isCarrying = false;
    private float originalYPosition; // ��������� ������������ Y-������� ������������ �������

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * raycastOffset; // ��������������� ��������� ������� ��������
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, grabDistance))
        {
            Debug.DrawRay(raycastOrigin, transform.forward * hit.distance, Color.green); // ������ ���

            if (hit.collider.CompareTag("Movable"))
            {
                grabbedObject = hit.collider.GetComponent<Rigidbody>();
                if (grabbedObject != null)
                {
                    joint = gameObject.AddComponent<ConfigurableJoint>();
                    joint.connectedBody = grabbedObject;

                    // ����������� ����������� �� ����������� � ��������
                    joint.xMotion = ConfigurableJointMotion.Locked;
                    joint.yMotion = ConfigurableJointMotion.Locked;
                    joint.zMotion = ConfigurableJointMotion.Locked;
                    joint.angularXMotion = ConfigurableJointMotion.Locked;
                    joint.angularYMotion = ConfigurableJointMotion.Locked;
                    joint.angularZMotion = ConfigurableJointMotion.Locked;

                    originalYPosition = grabbedObject.position.y; // ��������� ������������ Y-�������

                    isCarrying = true;
                    animator.SetBool("IsCarrying", isCarrying);
                }
            }
        }
        else
        {
            Debug.DrawRay(raycastOrigin, transform.forward * grabDistance, Color.red); // ������ ���
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Destroy(joint);
            grabbedObject = null;

            isCarrying = false;
            animator.SetBool("IsCarrying", isCarrying);
        }
    }

    void FixedUpdate()
    {
        if (grabbedObject != null)
        {
            // ������� ����������� ������� � ����� ���������
            Vector3 targetPosition = holdPoint.position;
            targetPosition.y = originalYPosition;

            // ���������� MovePosition ��� �������� �����������
            grabbedObject.MovePosition(Vector3.Lerp(grabbedObject.position, targetPosition, Time.fixedDeltaTime * 10f));
        }
    }
}
