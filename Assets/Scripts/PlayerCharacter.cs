using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Player Stats")]
    public int level = 1;
    public float lastCastTime;

    private float experience = 0f;
    private float experienceToNextLevel = 100f;
    private float health = 100f;
    private float mana = 50f;
    private float lastManaRegenTime;

    [Header("Mana Regen Settings")]
    [SerializeField] private float manaRegenRate = 5f;
    [SerializeField] private float manaRegenCooldown = 1f;

    [Header("Spells")]
    public ClassSpellList classSpellList;
    public List<SpellData> spellBook = new List<SpellData>();

    private List<(SpellData buff, float endTime)> activeBuffs = new List<(SpellData, float)>();

    public float Health { get => health; set => health = Mathf.Max(0, value); }
    public float Mana { get => mana; set => mana = Mathf.Max(0, value); }
    public int Level { get => level; set => level = Mathf.Max(1, value); }
    public float Experience
    {
        get => experience;
        set
        {
            experience = value;
            if (experience >= experienceToNextLevel) LevelUp();
        }
    }

    private void Start()
    {
        lastManaRegenTime = Time.time;

        UnlockSpellsForLevel(Level);
        Debug.Log("Player initialized with Level: " + level + "spell count: " + spellBook.Count + " spells.");
    }

    private void Update()
    {
        HandleManaRegen();
        HandleInput();
        UpdateActiveBuffs();
    }

    private void HandleManaRegen()
    {
        if (Time.time - lastManaRegenTime >= manaRegenCooldown)
        {
            Mana += manaRegenRate * Time.deltaTime;
            lastManaRegenTime = Time.time;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.R)) CastSpell(0);
        if (Input.GetKeyDown(KeyCode.T)) CastSpell(1);
    }

    private void UpdateActiveBuffs()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            if (Time.time >= activeBuffs[i].endTime)
            {
                RemoveBuff(activeBuffs[i].buff);
                activeBuffs.RemoveAt(i);
            }
        }
    }

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
                var buffSpell = spell as SelfBuffSpellData;
                activeBuffs.Add((spell, Time.time + buffSpell.buffDuration));
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

    private void RemoveBuff(SpellData spell)
    {
        var buffSpell = spell as SelfBuffSpellData;
        if (buffSpell == null)
        {
            Debug.LogWarning("Tried to remove a buff that is not a SelfBuffSpellData!");
            return;
        }

        switch (buffSpell.buffType)
        {
            case BuffType.Health:
                Health /= 1 + buffSpell.buffAmount;
                break;
            case BuffType.Mana:
                Mana /= 1 + buffSpell.buffAmount;
                break;
            case BuffType.Speed:
                var move = GetComponent<Move>();
                if (move != null)
                {
                    move.playerSpeed /= 1 + buffSpell.buffAmount;
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
    }

    private void LevelUp()
    {
        Level++;
        experience -= experienceToNextLevel;
        experienceToNextLevel *= 1.2f;
        health += 20;
        mana += 10;

        Debug.Log($"Leveled up to {Level}! Health: {health}, Mana: {mana}");

        UnlockSpellsForLevel(Level);
    }

    private void UnlockSpellsForLevel(int level)
    {
        foreach (SpellData spell in classSpellList.spells)
        {
            if (spell.requiredLevel == level && !spellBook.Contains(spell))
            {
                spellBook.Add(spell);
                Debug.Log("Unlocked spell: " + spell.spellName);
            }
        }
    }
}
