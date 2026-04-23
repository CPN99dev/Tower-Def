using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;
    public WaveData[] waves; 
    private int waveIndex = 0;



    public void StartNextWave()
    {
        if (waveIndex < waves.Length)
        {
            spawner.StartWave(waves[waveIndex]);
            waveIndex++;
        }
        else
        {
            Debug.Log("All waves completed!");
        }
    }

    public static GameManager Instance;

    [Header("Data")]
    public UserData data;
    private string savePath;

    [Header("UI Reference")]
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        Instance = this;
        savePath = Path.Combine(Application.persistentDataPath, "userdata.json");
        LoadGame();
        data.currentMoney = 1000; // Starting money for testing
    }

    // --- LOGIC CHECK ---
    public bool CanAfford(int amount) => data.currentMoney >= amount;

    public void SpendMoney(int amount)
    {
        data.currentMoney -= amount;
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        data.currentMoney += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (moneyText != null) moneyText.text = "$" + data.currentMoney;
    }

    // ---  SAVE / LOAD JSON ---
    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved to: " + savePath);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("Game Loaded");
        }
        else
        {
            data = new UserData(); 
        }
        UpdateUI();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}