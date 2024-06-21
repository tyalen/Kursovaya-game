using UnityEngine;

public class CameraClippingExample : MonoBehaviour
{
    public float nearDistance = 0.1f; // ���������� ������� ������������ ���������

    void Start()
    {
        Camera mainCamera = GetComponent<Camera>();
        mainCamera.nearClipPlane = nearDistance;
    }

    void Update()
    {
        // ������ ������������ ��������� ������� ������������ ���������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera mainCamera = GetComponent<Camera>();
            mainCamera.nearClipPlane = 0.5f; // ����� �������� ������� ������������ ���������
        }
    }
}
