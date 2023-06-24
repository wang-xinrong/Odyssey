using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Image currentWeaponIcon;
    public Image newWeaponIcon;
    public Image characterIcon;
    public GameObject SwapInterface;
    public GameObject SwapCharacterPrompt;
    public GameObject DamageTextPrefab;
    public GameObject HealthTextPrefab;

    public Canvas GameCanvas;

    private void Awake()
    {
        // should work as long as there is only one
        // canvas that is currently active
        GameCanvas = FindObjectOfType<Canvas>();

        // new, for weapon display
        GameObject player = GameObject.Find("Player");
        MainPlayerController script = player.GetComponent<MainPlayerController>();
        //currentWeaponIcon = GetComponent<Image>();
        if (!script)
        {
            return;
        }
        script.OnDisplayCurrentWeapon.AddListener(DisplayCurrentWeapon);
        script.OnDisplayCurrentCharacter.AddListener(DisplayCharacterIcon);
    }


    private void OnEnable()
    {
        CharacterEvents.CharacterHurt += CharacterHurt;
        CharacterEvents.CharacterHeal += CharacterHealed;

        // new, for weapon display
        WeaponPickup.OnDisplayDroppedWeapon += DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay += StopDisplayingDroppedWeapon;
        WeaponPickup.OnDisplaySwapCharacterPrompt += DisplaySwapCharacterPrompt;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterHurt -= CharacterHurt;
        CharacterEvents.CharacterHeal -= CharacterHealed;

        // new, for weapon display
        WeaponPickup.OnDisplayDroppedWeapon -= DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay -= StopDisplayingDroppedWeapon;
        WeaponPickup.OnDisplaySwapCharacterPrompt -= DisplaySwapCharacterPrompt;
    }

    void DisplayCharacterIcon(string charName)
    {
        characterIcon.sprite = Resources.Load<Sprite>(charName);
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

    public void CharacterHurt(GameObject character, int damageReceived)
    {
        // set the position of the DamageText to be where
        // the character was hit
        Vector3 _spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text _tmpText = Instantiate(DamageTextPrefab, _spawnPosition
                                , Quaternion.identity, GameCanvas.transform)
                                .GetComponent<TMP_Text>();

        /*
        // object pooling wo uld most likely be needed here,
        // since there can be multiple damage source the
        // player is receiving at a time.
        DamageText.transform.position = _enablePosition + DamageText.transform.parent.transform.position;
        DamageText.GetComponent<TMP_Text>().text = damageReceived.ToString();

        // reset the colour such that the text can be seen
        TMP_Text _healthText = DamageText.GetComponent<TMP_Text>();
        _healthText.color = new Color(_healthText.color.r, _healthText.color.g
            , _healthText.color.b, 1);
        DamageText.SetActive(true);
        Debug.Log("active");
        */
        _tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 _spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text _tmpText = Instantiate(HealthTextPrefab, _spawnPosition
                                , Quaternion.identity, GameCanvas.transform)
                                .GetComponent<TMP_Text>();

        _tmpText.text = healthRestored.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
