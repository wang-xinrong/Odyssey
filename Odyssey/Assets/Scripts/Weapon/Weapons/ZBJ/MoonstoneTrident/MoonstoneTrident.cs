using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonstoneTrident : Weapon
{
    void Awake()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new MoonstoneTridentAttack0(), new MoonstoneTridentAttack1(), new MoonstoneTridentAttack2()};
        CharIdle = "MoonstoneIdle";
        CharHurt = "MoonstoneHurt";
        CharDeath = "MoonstoneDeath";
        CharWalk = "MoonstoneWalk";
        CharSpecial = "MoonstoneSpecial";
        SpritePath = "ZBJ/zbjMoonstoneTrident";
        character = "zbj";
    }
}
