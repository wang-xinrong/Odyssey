using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public int HealthIncrease;
    public int SPIncrease;
    public int DamageIncrease;
    public float MovementIncrease;
    public float Duration;

    [Header("Only UI")]
    public bool Stackable = false;

    [Header("Both")]
    public string Name;
    public Sprite Image; 
}
