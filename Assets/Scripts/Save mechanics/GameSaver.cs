using UnityEngine;

public class GameSaver : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
                SaveSystem.Save(player);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            var data = SaveSystem.Load();
            if (data != null)
            {
                GameObject player = GameObject.Find("Player");
                if (player != null)
                {
                    player.transform.position = new Vector3(data.playerPos[0], data.playerPos[1], data.playerPos[2]);
                    player.GetComponent<PlayerCharacter>().Health = data.health;
                    Debug.Log("Game loaded!");
                }
            }
        }
    }
}
