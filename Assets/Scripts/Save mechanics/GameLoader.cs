using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject firePrefab;
    public GameObject icePrefab;
    public GameObject necroPrefab;

    [System.Obsolete]
    void Awake()
    {
        // Get the choice saved in CharacterSelect scene
        var choice = PlayerPrefs.GetString("SelectedCharacter", "Fire");

        // Pick prefab
        GameObject prefab = choice switch
        {
            "Ice" => icePrefab,
            "Necromancy" => necroPrefab,
            _ => firePrefab
        };

        // Spawn player
        GameObject playerGO = Instantiate(prefab);

        // Hook it up to GameManager
        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.player = playerGO.transform;
            gm.playerCharacter = playerGO.GetComponent<PlayerCharacter>();
        }
    }
}
