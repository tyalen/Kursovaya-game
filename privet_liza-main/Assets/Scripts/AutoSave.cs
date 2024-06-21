using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public Vector3 playerPosition;
}

public class AutoSave : MonoBehaviour
{
    public static AutoSave instance;

    private GameSaveData saveData;

    public Transform playerTransform;

    private float saveInterval = 600f; // �������� ���������� � �������� (600 ������ = 10 �����)
    private float saveTimer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // �������� ���������� ��� ������ ����
        LoadGame();

        // ������ ������� ��� ��������������� ����������
        saveTimer = saveInterval;
    }

    void Update()
    {
        // ���������� �������
        saveTimer -= Time.deltaTime;

        // ���� ������ �����, ��������� ���� � ���������� ������
        if (saveTimer <= 0f)
        {
            SaveGame();
            saveTimer = saveInterval;
        }
    }

    public void SaveGame()
    {
        saveData = new GameSaveData();
        saveData.playerPosition = GetPlayerPosition(); // ��������� ������� ������

        string saveJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("saveData", saveJson);
        PlayerPrefs.Save();

        Debug.Log("Game saved.");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("saveData"))
        {
            string saveJson = PlayerPrefs.GetString("saveData");
            saveData = JsonUtility.FromJson<GameSaveData>(saveJson);

            // ����������� ������ �� ���������� �������
            MovePlayerToSavedPosition(saveData.playerPosition);
        }
        else
        {
            Debug.Log("No save data found.");
            // ��������, ��������� �������� �� ���������, ���� ��� ����������
        }
    }

    private Vector3 GetPlayerPosition()
    {
        if (playerTransform != null)
        {
            return playerTransform.position;
        }
        else
        {
            Debug.LogError("Player transform is not assigned!");
            return Vector3.zero;
        }
    }

    private void MovePlayerToSavedPosition(Vector3 position)
    {
        playerTransform.position = position;
    }
}
