using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Spells/SelfBuffSpell")]
public class SelfBuffSpellData : SpellData
{
    public float buffAmount;
    public float buffDuration;
    public BuffType buffType;
}