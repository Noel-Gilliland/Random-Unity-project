using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatusDisplay : MonoBehaviour
{
   public PlayerCharacter character;   // Drag your player here
    public TMP_Text statusText;             // Drag your Text UI here

    void Update()
    {
        if (character != null)
        {
            float level = character.level;
            statusText.text = $"Level: {level:F1}";
        }
    }
}
