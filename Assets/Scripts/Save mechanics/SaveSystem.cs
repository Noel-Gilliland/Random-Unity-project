using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string characterName;
    public string sceneName;
    public float[] position;
    public float health;
    
}

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save(GameObject player)
    {
        SaveData data = new SaveData();
        data.characterName = PlayerPrefs.GetString("SelectedCharacter");
        data.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Vector3 pos = player.transform.position;
        data.position = new float[] { pos.x, pos.y, pos.z };
        data.health = player.GetComponent<PlayerCharacter>().Health;


        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Game saved to " + SavePath);
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("No save file found.");
            return null;
        }

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
