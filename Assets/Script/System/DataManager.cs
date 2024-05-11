using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 데이터 파일 경로 설정
    public string dataFilePath = "Assets/Data/GameData.csv";

    // 데이터 로드
    public GameData LoadGameData()
    {
        GameData data = new GameData();

        if (File.Exists(dataFilePath))
        {
            using (StreamReader reader = new StreamReader(dataFilePath))
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                data.data1 = float.Parse(values[0]);
                data.data2 = float.Parse(values[1]);
                data.data3 = float.Parse(values[2]);
                Debug.Log(data.data1);
                Debug.Log(data.data2);
                Debug.Log(data.data3);
            }
        }
        else
        {
            Debug.Log("파일에 오류가 발생했습니다");
        }

        return data;
    }

    // 데이터 저장
    public void SaveGameData(GameData data)
    {
        using (StreamWriter writer = new StreamWriter(dataFilePath))
        {
            string line = data.data1 + "," + data.data2 + "," + data.data3;
            writer.WriteLine(line);
        }
    }

    public void Load()
    {
        LoadGameData();
    }

    public void Save()
    {
        GameData data = new GameData(1,2,3);
        SaveGameData(data);
    }

}

[System.Serializable]
public class GameData
{
    public float data1;
    public float data2;
    public float data3;

    public GameData(float data1 = 0, float data2 = 0, float data3 = 0)
    {
        this.data1 = data1;
        this.data2 = data2;
        this.data3 = data3;
    }

}
