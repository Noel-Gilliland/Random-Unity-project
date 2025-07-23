using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBuffSpell : SpellBehaviour
{
    public override void Cast(Transform caster, SpellData spell)
    {
        Debug.Log("Buff applied to self!");
        if (spell.visualEffectPrefab)
            Instantiate(spell.visualEffectPrefab, caster.position, Quaternion.identity);


        SelfBuffSpellData buffSpell = spell as SelfBuffSpellData;
        if (buffSpell == null)
        {
            Debug.LogWarning("Spell is not a SelfBuffSpellData!");
            return;
        }
        // Apply the buff effect to the player character
        PlayerCharacter playerCharacter = caster.GetComponent<PlayerCharacter>();

        if (playerCharacter == null)
        {
            Debug.LogWarning("PlayerCharacter component not found on caster.");
            return;
        }

        switch (buffSpell.buffType)
        {
            case BuffType.Health:
                playerCharacter.Health *= 1 + buffSpell.buffAmount;
                Debug.Log("Health buff applied: " + buffSpell.buffAmount);
                break;
            case BuffType.Mana:
                playerCharacter.Mana *= 1 + buffSpell.buffAmount;
                Debug.Log("Mana buff applied: " + buffSpell.buffAmount);
                break;
            case BuffType.Speed:
                Move move = playerCharacter.GetComponent<Move>();
                if (move != null)
                {
                    move.playerSpeed *=  1 + buffSpell.buffAmount;
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


    
    }
}
