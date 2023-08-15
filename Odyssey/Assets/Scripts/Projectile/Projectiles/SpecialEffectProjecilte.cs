using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectProjecilte : Projectile
{
    public float Duration;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        SetUpDuration();
    }

    protected void SetUpDuration()
    {
        Duration = StatsManager.Instance.GetProjectileDuration(NameString);
    }
}
