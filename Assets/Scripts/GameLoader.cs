using UnityEngine;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        string characterName = PlayerPrefs.GetString("SelectedCharacter", "Warrior");

        GameObject prefab = Resources.Load<GameObject>("Characters/" + characterName);
        if (prefab != null)
        {
            GameObject player = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            player.name = "Player";
        }
        else
        {
            Debug.LogError("Character prefab not found: " + characterName);
        }
    }
}
