using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    private PlayerCharacter playerCharacter; // Assign in Inspector or find dynamically
    public List<Image> spellIcons; // Assign in Inspector, one for each slot
    public List<TextMeshProUGUI> cooldownTexts; // Assign in Inspector, one for each slot

    private float[] cooldownTimers;

    public List<Button> hotbarButtons; // Assign in Inspector, one for each slot
    void Start()
    {
        cooldownTimers = new float[spellIcons.Count];
        if (playerCharacter == null)
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            playerCharacter = playerObj.GetComponent<PlayerCharacter>();
    }
    }

    void Update()
{
    if (playerCharacter == null) return;

    for (int i = 0; i < hotbarButtons.Count; i++)
    {
            Debug.Log("Hello");
        if (i < playerCharacter.spellBook.Count)
            {
                hotbarButtons[i].gameObject.SetActive(true);

                var spell = playerCharacter.spellBook[i];

                // Set spell icon if available
                if (spell.icon != null)
                    spellIcons[i].sprite = spell.icon;

                // Handle cooldown display
                float timeSinceCast = Time.time - playerCharacter.lastCastTime;
                float cooldown = spell.cooldown;
                float remaining = Mathf.Clamp(cooldown - timeSinceCast, 0, cooldown);

                if (remaining > 0)
                {
                    cooldownTexts[i].text = remaining.ToString("F1");
                    spellIcons[i].color = new Color(1f, 1f, 1f, 0.5f); // faded
                }
                else
                {
                    cooldownTexts[i].text = "";
                    spellIcons[i].color = Color.white;
                }
            }
            else
            {
                // Hide unused hotbar buttons
                hotbarButtons[i].gameObject.SetActive(false);

                // Clear icon and text just in case
                spellIcons[i].sprite = null;
                spellIcons[i].color = Color.clear;
                cooldownTexts[i].text = "";
            }
    }
}

}