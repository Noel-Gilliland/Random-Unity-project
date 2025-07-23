using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    public List<Image> spellIcons; // Assign in Inspector, one for each slot
    public List<TextMeshProUGUI> cooldownTexts; // Assign in Inspector, one for each slot

    private float[] cooldownTimers;

    public List<Button> hotbarButtons; // Assign in Inspector, one for each slot
    void Start()
    {
        cooldownTimers = new float[spellIcons.Count];
    }

    void Update()
    {
        for (int i = 0; i < hotbarButtons.Count; i++)
    {
        if (i < playerCharacter.spellBook.Count)
        {
            hotbarButtons[i].gameObject.SetActive(true);
            // Optionally set icon, text, etc. here
            // Example: spellIcons[i].sprite = playerCharacter.spellBook[i].icon;
        }
        else
        {
            hotbarButtons[i].gameObject.SetActive(false);
        }
    }
        for (int i = 0; i < spellIcons.Count; i++)
        {
            if (i < playerCharacter.spellBook.Count)
            {
                var spell = playerCharacter.spellBook[i];
                // Set icon (if you have icons, otherwise skip)
                // spellIcons[i].sprite = spell.icon;

                // Cooldown logic
                float timeSinceCast = Time.time - playerCharacter.lastCastTime;
                float cooldown = spell.cooldown;
                float remaining = Mathf.Clamp(cooldown - timeSinceCast, 0, cooldown);

                if (remaining > 0)
                {
                    cooldownTexts[i].text = remaining.ToString("F1");
                    spellIcons[i].color = new Color(1, 1, 1, 0.5f); // faded when on cooldown
                }
                else
                {
                    cooldownTexts[i].text = "";
                    spellIcons[i].color = Color.white;
                }
            }
            else
            {
                cooldownTexts[i].text = "";
                spellIcons[i].color = Color.clear;
            }
        }
    }
}