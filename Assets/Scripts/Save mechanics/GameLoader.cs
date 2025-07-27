using UnityEngine;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        string characterName = PlayerPrefs.GetString("SelectedCharacter");

        GameObject prefab = Resources.Load<GameObject>("Characters/" + characterName);
        if (prefab != null)
        {
            Vector3 spawnPosition = new Vector3(51, -607, -302); // Set your desired spawn position

            GameObject player = Instantiate(prefab, spawnPosition, Quaternion.identity);
            player.name = "Player";
        }
        else
        {
            Debug.LogError("Character prefab not found: " + characterName);
        }
    }
}
