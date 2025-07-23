using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// This tells Unity that this is a special data-only class
[CreateAssetMenu(menuName = "Spells/Spell")]
public abstract class SpellData : ScriptableObject
{
    public string spellName;
    public float cooldown;
    public GameObject visualEffectPrefab;
    public SpellType spellType;
    public int requiredLevel;

    
}

public enum SpellType{
    SingleTarget,
    AreaCone,
    SelfBuff

}

public enum BuffType
{
    Health,
    Mana,
    Speed
}




