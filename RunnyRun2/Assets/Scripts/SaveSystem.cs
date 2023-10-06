using System.IO;
using UnityEngine;

/// <summary>
/// This class is used for saving and loading data.
/// It is used in conjunction with the Data class.
/// </summary>
public static class SaveSystem
{
    private const string FILE_EXT = ".json";
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static string Load(string fileName)
    {
        string fileLocation = SAVE_FOLDER + fileName + FILE_EXT;
        if (!File.Exists(fileLocation))
        {
            return null;
        }
        return File.ReadAllText(fileLocation);
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
}
