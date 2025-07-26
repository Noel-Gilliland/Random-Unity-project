using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerCharacter : MonoBehaviour
{
    public int level = 1;
    private float experience = 0f;
    private float experienceToNextLevel = 100f;
    private float health = 100f;
    private float mana = 50f;
    private float manaRegenRate = 5f; // Mana regeneration per second
    private float manaRegenCooldown = 1f; // Cooldown for mana regeneration
    private float lastManaRegenTime;
    public float lastCastTime;

    private List<(SpellData buff, float endTime)> activeBuffs = new List<(SpellData, float)>();
    public List<SpellData> allSpells;
    public List<SpellData> spellBook = new List<SpellData>();


    public void CastSpell(int index)
    {
        if (index < 0 || index >= spellBook.Count)
        {
            Debug.LogWarning("Invalid spell index.");
            return;
        }

        SpellData spell = spellBook[index];

        if (Time.time - lastCastTime < spell.cooldown) return;

        lastCastTime = Time.time;

        GameObject spellObject = new GameObject("Spell_" + spell.spellName);

        SpellBehaviour behavior = null;

        switch (spell.spellType)
        {
            case SpellType.AreaCone:
                behavior = spellObject.AddComponent<AreaConeSpell>();
                break;
            case SpellType.SelfBuff:
                behavior = spellObject.AddComponent<SelfBuffSpell>();
                float duration = ((SelfBuffSpellData)spell).buffDuration;
                activeBuffs.Add((spell, Time.time + duration));
                Debug.Log("Spell added to active buffs: " + spell.spellName);
                break;
            case SpellType.SingleTarget:
                // future implementation
                break;
        }

        if (behavior != null)
            behavior.Cast(transform, spell);

        Destroy(spellObject, 1f);
    }


    public float Health
    {
        get { return health; }
        set { health = Mathf.Max(0, value); } // Ensure health doesn't go below 0
    }
    public float Mana
    {
        get { return mana; }
        set { mana = Mathf.Max(0, value); } // Ensure mana doesn't go below 0
    }
    public int Level
    {
        get { return level; }
        set { level = Mathf.Max(1, value); } // Ensure level doesn't go below 1
    }
    public float Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            if (experience >= experienceToNextLevel)
            {
                LevelUp();
            }
        }
    }
    private void LevelUp()
    {
        Level++;
        experience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f; // Increase the required experience for the next level
        health += 20; // Increase health on level up
        mana += 10; // Increase mana on level up
        Debug.Log("Leveled up to " + Level + "! Health: " + health + ", Mana: " + mana);

        UnlockSpellsForLevel(Level);

    }
    private void Start()
    {
        lastManaRegenTime = Time.time;
        UnlockSpellsForLevel(Level);
    }
    private void Update()
    {
        // Regenerate mana over time
        if (Time.time - lastManaRegenTime >= manaRegenCooldown)
        {
            Mana += manaRegenRate * Time.deltaTime;
            lastManaRegenTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CastSpell(0); // Cast first spell
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            CastSpell(1); // Cast second spell
        }
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            if (Time.time >= activeBuffs[i].endTime)
            {
                RemoveBuff(activeBuffs[i].buff);
                activeBuffs.RemoveAt(i);
            }
        }
    }
    private void UnlockSpellsForLevel(int level)
    {
        foreach (SpellData spell in allSpells)
        {
            if (spell.requiredLevel == level && !spellBook.Contains(spell))
            {
                spellBook.Add(spell);
                Debug.Log("Unlocked spell: " + spell.spellName);
            }
        }
    }
    private void RemoveBuff(SpellData buff)
    {
        // Reverse whatever effect the buff had
        SelfBuffSpellData buffSpell = buff as SelfBuffSpellData;
        if (buffSpell == null)
        {
            Debug.LogWarning("Tried to remove a buff that is not a SelfBuffSpellData!");
            return;
        }


        switch (buffSpell.buffType)
        {
            case BuffType.Health:
                Health /= 1 + buffSpell.buffAmount;
                Debug.Log("Health buff applied: " + buffSpell.buffAmount);
                break;
            case BuffType.Mana:
                Mana /= 1 + buffSpell.buffAmount;
                Debug.Log("Mana buff applied: " + buffSpell.buffAmount);
                break;
            case BuffType.Speed:
                Move move = GetComponent<Move>();
                if (move != null)
                {
                    move.playerSpeed /= 1 + buffSpell.buffAmount;
                    Debug.Log("Speed buff applied: " + buffSpell.buffAmount);
                }
                else
                {
                    Debug.LogWarning("Move component not found on player.");
                }
                break;
            default:
                Debug.LogWarning("Unknown buff type: " + buffSpell.buffType);
                break;
        }
        Debug.Log("Buff expired: " + buffSpell.spellName);
        // Example: If speed buff, set speed back to normal
    }

}
