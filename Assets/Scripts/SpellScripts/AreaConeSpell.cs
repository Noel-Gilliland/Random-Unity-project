using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaConeSpell : SpellBehaviour
{
    public override void Cast(Transform caster, SpellData spell)

    {
        float range = 5f;
        float angle = 45f;
        Collider[] hits = Physics.OverlapSphere(caster.position, range);

        AreaConeSpellData coneSpell = spell as AreaConeSpellData;
        if (coneSpell == null)
        {
            Debug.LogWarning("Spell is not an AreaConeSpellData!");
            return;
        }

        foreach (var hit in hits)
        {
            Vector3 directionToTarget = (hit.transform.position - caster.position).normalized;
            float angleToTarget = Vector3.Angle(caster.forward, directionToTarget);

            if (angleToTarget < angle)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(coneSpell.damage);
                    if (spell.visualEffectPrefab)
                        Instantiate(spell.visualEffectPrefab, enemy.transform.position, Quaternion.identity);
                }
            }
        }
    }
    
    
}
