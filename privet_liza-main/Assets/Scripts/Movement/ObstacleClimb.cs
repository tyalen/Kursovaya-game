using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleClimb : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float climbDistance = 1f; // Max distance to check for obstacles 
    public float climbHeight = 2f; // Height to climb onto the obstacle 
    public float forwardMoveDistance = 0.1f; // Distance to move forward after climbing 
    public LayerMask obstacleLayer; // Layer mask for obstacles 
    public LayerMask groundLayer; // Layer mask for ground 

    public bool isClimbing = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isClimbing)
        {
            TryClimb();
        }
    }

    void TryClimb()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, climbDistance, obstacleLayer))
        {
            Vector3 climbPosition = hit.point + hit.normal * 0.2f; // Adjust position slightly above the obstacle 
            climbPosition.y += climbHeight;

            StartCoroutine(ClimbAnimation(climbPosition));
        }
    }

    IEnumerator ClimbAnimation(Vector3 targetPosition)
    {
        isClimbing = true;
        animator.SetBool("IsClimbing", isClimbing); // Moved inside the coroutine
        Debug.Log("Started Climbing");

        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;
        float climbDuration = 0.4f;

        // Disable collision with obstacles during climbing animation 
        gameObject.layer = 2; // Ignore Raycast layer 

        while (elapsedTime < climbDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / climbDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isClimbing = false; // Устанавливаем isClimbing в false после завершения анимации прыжка
        animator.SetBool("IsClimbing", isClimbing); // Moved inside the coroutine

        Debug.Log("Finished Climbing");

        // Re-enable collision with obstacles after climbing animation 
        gameObject.layer = 0; // Default layer 

        // Check if there's ground ahead 
        RaycastHit groundHit;
        if (!Physics.Raycast(transform.position, transform.forward, out groundHit, forwardMoveDistance, groundLayer))
        {
            // Smoothly move forward 
            Vector3 targetForwardPosition = transform.position + transform.forward * forwardMoveDistance;
            yield return StartCoroutine(MoveForwardSmoothly(targetForwardPosition));
        }
    }

    IEnumerator MoveForwardSmoothly(Vector3 targetPosition)
    {
        float moveDuration = 0.1f;
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
