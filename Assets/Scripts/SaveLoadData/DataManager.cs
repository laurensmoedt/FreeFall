using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameData data;

    private readonly string file = "GameData.txt";

    private readonly int key = 6969;

    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        WriteToFile(file, SecureHelper.EncryptDecrypt(json, key));
    }

    public void Load()
    {
        data = new GameData();
        string json = ReadFromFile(file);
        json = SecureHelper.EncryptDecrypt(json, key);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    public void DeleteFile()
    {
        string path = GetFilePath(file);
        File.Delete(path);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }

        return "";
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
