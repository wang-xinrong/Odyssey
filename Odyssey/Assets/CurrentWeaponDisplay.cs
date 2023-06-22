using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponDisplay : MonoBehaviour
{
    public Image currentWeaponIcon;
    public GameObject SwapInterface;
    public Image newWeaponIcon;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject player = GameObject.Find("Player");
        MainPlayerController script = player.GetComponent<MainPlayerController>();
        currentWeaponIcon = GetComponent<Image>();
        script.DisplayCurrentWeapon.AddListener(UpdateCurrentWeapon);
        WeaponPickup.OnDisplay += DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay += StopDisplayingDroppedWeapon;
    }

    void StopDisplayingDroppedWeapon()
    {
        SwapInterface.SetActive(false);
    }

    void DisplayDroppedWeapon(Weapon weapon)
    {
        SwapInterface.SetActive(true);
        newWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
    void UpdateCurrentWeapon(Weapon weapon)
    {
        Debug.Log("updating");
        currentWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
}
