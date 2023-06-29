using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public delegate void DisplayDroppedWeapon(Weapon weapon);
    public static event DisplayDroppedWeapon OnDisplayDroppedWeapon;

    public delegate void DisplaySwapCharacterPrompt();
    public static event DisplaySwapCharacterPrompt OnDisplaySwapCharacterPrompt;

    public delegate void RemoveDisplay();
    public static event RemoveDisplay OnRemoveDisplay;

    public Weapon droppedWeapon;

    // start is only called after all existing objects' awake have been called,
    // thus start is used here to ensure the Weapon data has been set up before
    // WeaponPickup retrieve those data from Weapon
    void Start()
    {
        droppedWeapon = GetComponent<Weapon>();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(droppedWeapon.SpritePath);
    }

    public void UpdateSprite(Weapon weapon)
    {
        this.droppedWeapon = weapon;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(weapon.SpritePath);
        OnDisplayDroppedWeapon?.Invoke(weapon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController script = collision.GetComponent<PlayerController>();
        if (!script)
        {
            return;
        }
        if (script.charName == droppedWeapon.character)
        {
            OnDisplayDroppedWeapon?.Invoke(droppedWeapon);
            script.canPickUp = true;
            script.weaponOnFloor = this;
        } else {
            OnDisplaySwapCharacterPrompt?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnRemoveDisplay?.Invoke();
        PlayerController script = collision.GetComponent<PlayerController>();
        script.canPickUp = false;
    }
}
