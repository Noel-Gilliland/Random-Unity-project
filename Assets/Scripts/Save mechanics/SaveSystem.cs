using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class CharacterMeta {
    public string id;        // GUID
    public string name;      // “NoelFireMage”
    public string archetype; // “Fire”, “Ice”, “Necromancy”
    public string lastScene; // convenience (optional)
}

[System.Serializable]
public class RosterData
{
    public List<CharacterMeta> characters = new();
}

[System.Serializable]
public class SaveData
{
    public string characterId;
    public string sceneName;
    public float[] playerPos;
    public float health;
    
    public int level;
}

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/save.json";

    public static void Save(GameObject player)
    {
        SaveData data = new SaveData
        {
            characterId = PlayerPrefs.GetString("SelectedCharacter"),
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        };
        Vector3 pos = player.transform.position;
        data.playerPos = new float[] { pos.x, pos.y, pos.z };

        var pc = player.GetComponent<PlayerCharacter>();
        if (pc != null)
        {
            data.level = pc.level;
            data.health = pc.Health;
        }
        else
        {
            Debug.LogWarning("PlayerCharacter component not found on player object.");
        }
    


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
