using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private GameObject m_Camera;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 cameraOffset = Vector3.zero;
    [SerializeField] private Quaternion cameraRotation = Quaternion.identity;

    private void FixedUpdate()
    {
        m_Camera.transform.position = cameraOffset + player.transform.position;
        m_Camera.transform.rotation = cameraRotation;
    }
}
