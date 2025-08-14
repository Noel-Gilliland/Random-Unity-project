using System.Collections;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }
     public static MeshCollider meshCollider { get; private set; }

    public static bool IsReady => Instance != null;
    private void Awake()
    {
        if (Instance == null)
        {
            StartCoroutine(FindPlayerRoutine());
        }
    }

    private IEnumerator FindPlayerRoutine()
    {
        while (Instance == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player"); // or FindWithTag("Player")
            if (playerObj != null)
            {
                Instance = playerObj.GetComponentInChildren<PlayerCharacter>();
                meshCollider = playerObj.GetComponentInChildren<MeshCollider>();

                Debug.Log("[PlayerLocator] PlayerCharacter found.");
            }

            yield return null;
        }
    }

    public static IEnumerator WaitForPlayer()
    {
        while (!IsReady)
            yield return null;
    }
}
