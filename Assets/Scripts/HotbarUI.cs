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

    // How many slots are safe to access across ALL lists + spellBook
    int slotCount = Mathf.Min(
        hotbarButtons.Count,
        spellIcons.Count,
        cooldownTexts.Count,
        playerCharacter.spellBook != null ? playerCharacter.spellBook.Count : 0
    );

    // Update only safe indices
    for (int i = 0; i < slotCount; i++)
    {
        hotbarButtons[i].gameObject.SetActive(true);

        var spell = playerCharacter.spellBook[i];

        // icon
        if (spell.icon != null)
            spellIcons[i].sprite = spell.icon;

        // cooldown (uses your existing single lastCastTime)
        float remaining = Mathf.Clamp(spell.cooldown - (Time.time - playerCharacter.lastCastTime), 0f, spell.cooldown);
        cooldownTexts[i].text = remaining > 0f ? remaining.ToString("F1") : "";
        spellIcons[i].color = remaining > 0f ? new Color(1f,1f,1f,0.5f) : Color.white;
    }

    // Hide any extra UI slots beyond what we can safely fill
    for (int i = slotCount; i < hotbarButtons.Count; i++)
    {
        hotbarButtons[i].gameObject.SetActive(false);
    }
}


}