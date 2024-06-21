using UnityEngine;

public class CameraClippingExample : MonoBehaviour
{
    public float nearDistance = 0.1f; // Расстояние ближней клиппинговой плоскости

    void Start()
    {
        Camera mainCamera = GetComponent<Camera>();
        mainCamera.nearClipPlane = nearDistance;
    }

    void Update()
    {
        // Пример динамической настройки ближней клиппинговой плоскости
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera mainCamera = GetComponent<Camera>();
            mainCamera.nearClipPlane = 0.5f; // Новое значение ближней клиппинговой плоскости
        }
    }
}
