using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyUnderSpecialEffect
{
    // the character would have its moving speed reduced
    public void SlowedDown(float fractionOfOriginalSpeed, float duration);
}

