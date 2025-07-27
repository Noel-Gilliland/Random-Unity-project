using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Spells/ClassSpellList")]
public class ClassSpellList : ScriptableObject
{
    public string className;
    public List<SpellData> spells;

    // You can add methods to manage the spell list if needed
    public void AddSpell(SpellData spell)
    {
        if (!spells.Contains(spell))
        {
            spells.Add(spell);
        }
    }

    public void RemoveSpell(SpellData spell)
    {
        if (spells.Contains(spell))
        {
            spells.Remove(spell);
        }
    }
}

