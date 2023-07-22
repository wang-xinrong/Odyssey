using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilTrident : Weapon
{
    void Awake()
    {
        comboCount = 3;
        combos = new WeaponAttack[] {new DevilTridentAttack0(), new DevilTridentAttack1(), new DevilTridentAttack2()};
        CharIdle = "DevilIdle";
        CharHurt = "DevilHurt";
        CharDeath = "DevilDeath";
        CharWalk = "DevilWalk";
        CharSpecial = "DevilSpecial";
        SpritePath = "ZBJ/zbjDevilTrident";
        character = "zbj";
    }
}
