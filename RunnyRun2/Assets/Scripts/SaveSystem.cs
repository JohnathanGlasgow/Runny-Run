using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public static readonly string FILE_EXT = ".json";

    public static void Init()
    {

    }

    public static void Save(string fileName, object saveData)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, json);
    }

    public static string Load(string fileName)
    {
        string fileLocation = SAVE_FOLDER + fileName + FILE_EXT;
        if (!File.Exists(fileLocation))
        {
            return null;
        }
        string json = File.ReadAllText(fileLocation);
        return json;
    }
}
