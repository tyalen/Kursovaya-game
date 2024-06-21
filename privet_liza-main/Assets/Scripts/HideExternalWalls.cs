using UnityEngine;

public class HideExternalWalls : MonoBehaviour
{
    public GameObject externalWalls; // ������ �� GameObject � �������� ������� ������
    public Transform player; // ������ �� Transform ���������

    public float hideDistance = 10f; // ����������, ��� ������� ��������� ���� ����� ��������

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < hideDistance)
        {
            externalWalls.SetActive(false); // ��������� ��������� ������� ����
        }
        else
        {
            externalWalls.SetActive(true); // �������� ��������� ������� ����
        }
    }
}
