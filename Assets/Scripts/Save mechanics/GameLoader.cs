using UnityEngine;
using System.Collections;
public class GameLoader : MonoBehaviour

{
    void Start()
    {
        string characterName = PlayerPrefs.GetString("SelectedCharacter");



        GameObject prefab = Resources.Load<GameObject>("Characters/" + characterName);
        if (prefab != null)
        {
            StartCoroutine (Spawnposition(prefab));
            
        }
        else
        {
            Debug.LogError("Character prefab not found: " + characterName);
        }
    }
    IEnumerator Spawnposition(GameObject prefab)
    {
        GameObject spawnposition = null;
        while (spawnposition == null)
        {
            spawnposition = GameObject.Find("spawnlocation"); // Wait for 2 seconds
            yield return null;
        }

            GameObject spawnball = GameObject.Find("spawnlocation");
            Vector3 spawnPosition = spawnball.GetComponent<Transform>().position;
            //*Vector3 spawnPosition = new Vector3(51, -607, -302); // Set your desired spawn position
            GameObject player = Instantiate(prefab, spawnPosition, Quaternion.identity);
            player.name = "player";
        
        Debug.Log("I did it!");
    }
}
