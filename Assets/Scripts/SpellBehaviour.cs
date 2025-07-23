using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBehaviour : MonoBehaviour
{
    public abstract void Cast(Transform caster, SpellData spell);
    
}

