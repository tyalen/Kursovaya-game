using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float offset;

    [SerializeField] private LayerMask ledgeLayer;
    [SerializeField] private GameObject player;

    private readonly Collider[] ledgeCollider = new Collider[1];
    void Update()
    {
        
        var ledgeDetected = Physics.OverlapSphereNonAlloc(transform.position, radius, ledgeCollider, ledgeLayer) > 0;

        if (ledgeDetected)
        {
            Debug.Log("ledge");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
