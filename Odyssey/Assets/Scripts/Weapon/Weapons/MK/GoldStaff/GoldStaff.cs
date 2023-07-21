using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStaff : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new GoldStaffAttack0(), new GoldStaffAttack1(), new GoldStaffAttack2()};
        CharIdle = "GoldIdle";
        CharHurt = "GoldHurt";
        CharDeath = "GoldDeath";
        CharWalk = "GoldWalk";
        CharSpecial = "GoldSpecial";
        SpritePath = "MK/mkGoldStaff";
        character = "mk";
    }
}