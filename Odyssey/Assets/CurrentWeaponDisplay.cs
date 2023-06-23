using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CurrentWeaponDisplay : MonoBehaviour
{
    public Image currentWeaponIcon;
    public GameObject SwapInterface;
    public GameObject SwapCharacterPrompt;
    public Image newWeaponIcon;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject player = GameObject.Find("Player");
        MainPlayerController script = player.GetComponent<MainPlayerController>();
        currentWeaponIcon = GetComponent<Image>();
        if (script)
        {
            script.OnDisplayCurrentWeapon.AddListener(DisplayCurrentWeapon);
        }
        WeaponPickup.OnDisplayDroppedWeapon += DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay += StopDisplayingDroppedWeapon;
        WeaponPickup.OnDisplaySwapCharacterPrompt += DisplaySwapCharacterPrompt;
    }

    void DisplaySwapCharacterPrompt()
    {
        SwapCharacterPrompt.SetActive(true);
    }

    void StopDisplayingDroppedWeapon()
    {
        SwapCharacterPrompt.SetActive(false);
        SwapInterface.SetActive(false);
    }

    void DisplayDroppedWeapon(Weapon weapon)
    {
        SwapCharacterPrompt.SetActive(false);
        SwapInterface.SetActive(true);
        newWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
    void DisplayCurrentWeapon(Weapon weapon)
    {
        currentWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
}
