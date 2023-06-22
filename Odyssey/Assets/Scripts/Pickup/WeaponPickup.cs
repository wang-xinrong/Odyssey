using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponPickup : MonoBehaviour
{
    public delegate void Display(Weapon weapon);
    public static event Display OnDisplay;

    public delegate void RemoveDisplay();
    public static event RemoveDisplay OnRemoveDisplay;

    public Weapon droppedWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDisplay?.Invoke(droppedWeapon);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnRemoveDisplay?.Invoke();
    }
}
