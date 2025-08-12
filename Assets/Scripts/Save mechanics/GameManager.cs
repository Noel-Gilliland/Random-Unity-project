using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager fields
    [Header("Spawn")]
    public Transform spawnPoint;                  // drag a SpawnPoint in your scene (empty at start pos)
    public Vector3 fallbackSpawn = new Vector3(0, 1, 0);

    [Header("Player Prefabs (assign in Inspector)")]
    public GameObject firePrefab;
    public GameObject icePrefab;
    public GameObject necroPrefab;

    [Header("Runtime refs (auto-filled)")]
    public Transform player;                // auto-set after spawn
    public PlayerCharacter playerCharacter; // auto-set after spawn

    void Awake()
    {
        // 1) Pick prefab based on CharacterSelect choice
        var choice = PlayerPrefs.GetString("SelectedCharacter", "Fire");

        GameObject prefab = choice switch
        {
            "Ice" => icePrefab,
            "Necromancy" => necroPrefab,
            _ => firePrefab
        };

        // 2) Spawn it
        GameObject playerGO = Instantiate(prefab);
        player = playerGO.transform;
        playerCharacter = playerGO.GetComponentInChildren<PlayerCharacter>();
        
        player.position = spawnPoint ? spawnPoint.position : fallbackSpawn;
        // 3) Wire up references
        if (playerCharacter == null)
        {
            Debug.LogError("PlayerCharacter component not found on spawned player prefab.");
        }
    }

    void Start()
    {
        // 4) Restore from disk (if save exists)
        var data = SaveSystem.Load();
        if (data == null)
        {
            // First run defaults
            if (playerCharacter != null && playerCharacter.level <= 0)
                playerCharacter.level = 1;
            Debug.Log("[GameManager] No save found. Using defaults.");
            return;
        }

        // Restore stats
        if (playerCharacter != null)
        {
            playerCharacter.level = data.level > 0 ? data.level : 1;
            if (data.health > 0f) playerCharacter.Health = data.health;
        }

        // Restore position (safe with CharacterController)
        if (player != null && data.playerPos != null && data.playerPos.Length == 3)
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;

            player.position = new Vector3(data.playerPos[0], data.playerPos[1], data.playerPos[2]);

            if (cc) cc.enabled = true;
        }

        Debug.Log("[GameManager] Restore complete.");
    }

    public void SaveNow()
    {
        if (player == null) { Debug.LogWarning("SaveNow: no player to save."); return; }
        SaveSystem.Save(player.gameObject);
    }

    void OnApplicationQuit() => SaveNow();
    void OnApplicationPause(bool paused) { if (paused) SaveNow(); }
}

    /*
    public Transform player;                    // assign in Inspector
    public PlayerCharacter playerCharacter;     // your script with level, etc.

    void Start() {
        var data = SaveSystem.Load();
        if (data != null)
        {
            
        }
        if (SaveSystem.TryLoad(out var data))
        {
            // Restore level
            playerCharacter.level = data.level;

            // Restore position
            if (data.playerPos != null && data.playerPos.Length == 3)
            {
                player.position = new Vector3(data.playerPos[0], data.playerPos[1], data.playerPos[2]);
            }

            // Restore inventory (example)
            //playerCharacter.inventory = new List<string>(data.inventoryIds ?? new List<string>());
        }
        else
        {
            // First run defaults
            playerCharacter.level = 1;
            // playerCharacter.inventory = new List<string>();
        }
    }

    public void SaveNow() {
        SaveSystem.Save(player.gameObject);  // <-- pass the GameObject
    }

    

    void OnApplicationQuit() => SaveNow();
    void OnApplicationPause(bool paused) { if (paused) SaveNow(); } // mobile-safe
    */

