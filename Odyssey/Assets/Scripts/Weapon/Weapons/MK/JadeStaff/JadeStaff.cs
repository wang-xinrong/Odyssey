using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeStaff : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new JadeStaffAttack0(), new JadeStaffAttack1(), new JadeStaffAttack2()};
        CharIdle = "JadeIdle";
        CharHurt = "JadeHurt";
        CharDeath = "JadeDeath";
        CharWalk = "JadeWalk";
        CharSpecial = "JadeSpecial";
        SpritePath = "MK/mkJadeStaff";
    }
}
