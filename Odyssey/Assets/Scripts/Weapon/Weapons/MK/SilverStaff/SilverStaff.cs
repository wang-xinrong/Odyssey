using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverStaff : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new SilverStaffAttack0(), new SilverStaffAttack1(), new SilverStaffAttack2()};
        CharIdle = "SilverIdle";
        CharHurt = "SilverHurt";
        CharDeath = "SilverDeath";
        CharWalk = "SilverWalk";
        CharSpecial = "SilverSpecial";
        SpritePath = "MK/mkSilverStaff";
        character = "mk";
    }
}