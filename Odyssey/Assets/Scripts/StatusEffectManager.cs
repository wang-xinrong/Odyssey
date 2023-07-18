using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public static StatusEffectManager instance;
    public Queue<StatusEffect> availableGUIs = new Queue<StatusEffect>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        StatusEffect[] arr = GetComponentsInChildren<StatusEffect>();
        foreach (StatusEffect s in arr)
        {
            availableGUIs.Enqueue(s);
        }
    }

    public void DelegateToGUI(string status, int duration)
    {
        StatusEffect curr = availableGUIs.Dequeue();
        curr.DisplayStatusEffect(status, duration);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
