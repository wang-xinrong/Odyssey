using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrident : Weapon
{
    void Start()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new BasicTridentAttack0(), new BasicTridentAttack1(), new BasicTridentAttack2()};
    }
}
