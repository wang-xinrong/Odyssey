using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponDisplay : MonoBehaviour
{
    public Image icon;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        MainPlayerController script = player.GetComponent<MainPlayerController>();
        icon = GetComponent<Image>();
        script.DisplayCurrentWeapon.AddListener(UpdateIcon);
    }

    void UpdateIcon(Weapon weapon)
    {
        Debug.Log("here");
        icon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
}
