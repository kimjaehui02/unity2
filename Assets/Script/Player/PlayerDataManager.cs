using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.TextCore.Text;
using System.Text;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        if(!File.Exists(Application.dataPath+"/playerData.json"))
        {
            PlayerData playerdata = new PlayerData(100, 300, 4, 0, 0, null);
            string jsonData = JsonUtility.ToJson(playerdata);
            CreateJsonFile(Application.dataPath, "playerData", jsonData);
        }
        Debug.Log("Create");
        LoadPlayerDataFromJson();
    }

    [ContextMenu("Save")]
    public void SavePlayerDataToJson()
    {

        string jsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(path, jsonData);

    }
    [ContextMenu("Load")]
    void LoadPlayerDataFromJson()
    {

        string path = Path.Combine(Application.dataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

    }
    public void AddPlayerSoulsData(int amount)
    {
        playerData.playerSouls += amount; 
    }

    public void ChangePlayerItemsData(int[] itemsData)
    {
        playerData.itemsIndex= itemsData;
    }
    void CreateJsonFile(string createPath, string fileName, string jsonData) 
    { 
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData); 
        fileStream.Write(data, 0, data.Length); 
        fileStream.Close(); 
    }
}
[System.Serializable]
public class PlayerData
{
    public int playerMaxHp;
    public int playerMaxStamina;
    public int playerMaxSpearAmount;
    public int currentSceneIndex;
    public int playerSouls;
    public int[] itemsIndex;

    public PlayerData(int _playerMaxHp, int _playerMaxStamina, int _playerMaxSpearAmount, int _currentSceneIndex, int _playerSouls, int[] _itemsIndex)
    {
        playerMaxHp = _playerMaxHp;
        playerMaxStamina = _playerMaxStamina;
        playerMaxSpearAmount = _playerMaxSpearAmount;
        currentSceneIndex = _currentSceneIndex;
        playerSouls = _playerSouls;
        itemsIndex = _itemsIndex;
    }
}
