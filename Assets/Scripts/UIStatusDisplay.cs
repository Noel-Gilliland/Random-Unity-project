using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatusDisplay : MonoBehaviour
{
    private PlayerCharacter playerCharacter;   // Drag your player here
    public TMP_Text statusText;             // Drag your Text UI here

   void Start()
{
    StartCoroutine(Init());
}

IEnumerator Init()
{
    yield return PlayerLocator.WaitForPlayer();
    playerCharacter = PlayerLocator.Instance;
}


    void Update()
    {
        if (playerCharacter != null)
        {
            float level = playerCharacter.level;
            statusText.text = $"Level: {level}";
        }
    }
}
