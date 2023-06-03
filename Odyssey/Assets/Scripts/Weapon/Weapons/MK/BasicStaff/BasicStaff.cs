using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaff : Weapon
{
    void Start()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new BasicStaffAttack0(), new BasicStaffAttack1(), new BasicStaffAttack2()};
    }
}
