using UnityEngine;

public class HideExternalWalls : MonoBehaviour
{
    public GameObject externalWalls; // Ссылка на GameObject с внешними стенами здания
    public Transform player; // Ссылка на Transform персонажа

    public float hideDistance = 10f; // Расстояние, при котором рендеринг стен будет выключен

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < hideDistance)
        {
            externalWalls.SetActive(false); // Выключаем рендеринг внешних стен
        }
        else
        {
            externalWalls.SetActive(true); // Включаем рендеринг внешних стен
        }
    }
}
