using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 normal;

    public Vector3 Project(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, normal) * normal;
        //вектор вдоль поверхности
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Surface"))
        {
            normal = collision.contacts[0].normal;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + normal * 1f); //вектор нормали

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward));
    }
}
