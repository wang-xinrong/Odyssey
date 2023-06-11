using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnderSpecialEffect
{
    // the player would have his control reversed
    public void Bewitched(float duration);

    // the character would have its moving speed reduced
    public void SlowedDown(float fractionOfOriginalSpeed, float duration);
}
