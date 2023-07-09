using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTrident : Weapon
{
    void Awake()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new GoldTridentAttack0(), new GoldTridentAttack1(), new GoldTridentAttack2()};
        CharIdle = "GoldIdle";
        CharHurt = "GoldHurt";
        CharDeath = "GoldDeath";
        CharWalk = "GoldWalk";
        CharSpecial = "GoldSpecial";
        SpritePath = "ZBJ/zbjGoldTrident";
        character = "zbj";
    }
}
