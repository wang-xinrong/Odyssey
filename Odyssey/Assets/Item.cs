using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public float HealthIncrease;
    public float SPIncrease;
    public float MovementIncrease;
    public float DamageIncrease;

    [Header("Only UI")]
    public bool Stackable = false;

    [Header("Both")]
    public string Name;
    public Sprite Image; 
}
