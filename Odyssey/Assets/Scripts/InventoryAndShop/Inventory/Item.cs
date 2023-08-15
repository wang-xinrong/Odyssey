using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Only UI")]
    public bool Stackable = true;

    [Header("Both")]
    public string NameString;
    public Sprite Image;
    public int HealthIncrease;
    public int SPIncrease;
    public int DamageIncrease;
    public float MovementIncrease;
    public float Duration;
    public string Description;
    public int price;
    public enum Type { Speed, HP, SP, Damage}
    public Type ItemType;
}
