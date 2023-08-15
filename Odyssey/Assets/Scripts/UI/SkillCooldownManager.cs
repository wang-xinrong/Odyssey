using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCooldownManager : MonoBehaviour
{
    [SerializeField]
    public List<CooldownElement> cooldownElements;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void ReflectCooldown(int skillNumber, int duration)
    {
        cooldownElements[skillNumber].DisplayCooldown(duration);
    }
}
