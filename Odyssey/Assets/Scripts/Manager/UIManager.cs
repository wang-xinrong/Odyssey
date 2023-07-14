using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image currentWeaponIcon;
    public Image newWeaponIcon;
    public Image primaryCharacterIcon;
    public Image secondaryCharacterIcon;
    public Image currentCharacterIcon;
    public GameObject SwapInterface;
    public GameObject SwapCharacterPrompt;
    public GameObject DamageTextPrefab;
    public GameObject HealthTextPrefab;
    public GameObject DialogueUI;
    public TMP_Text DialogueText;
    public GameObject playerDialogueIcon;
    public GameObject otherDialogueIcon;

    public bool IsCutSceneOn = false;

    public Canvas GameCanvas;
    public GameObject HealthDamageTextManager;

    public List<Dialogue> currDialogues; 
    private int currDialogueIndex = -1;

    public void LoadScene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    private void Awake()
    {
        // should work as long as there is only one
        // canvas that is currently active
        GameCanvas = FindObjectOfType<Canvas>();

        // new, for weapon display
        // GameObject player = GameObject.Find("Player");
        // MainPlayerController script = player.GetComponent<MainPlayerController>();
        //currentWeaponIcon = GetComponent<Image>();
        // if (!script)
        // {
        //     return;
        // }
        // script.OnDisplayCurrentWeapon.AddListener(DisplayCurrentWeapon);
        // script.OnDisplayCurrentCharacter.AddListener(DisplayCharacterIcon);
    }


    private void OnEnable()
    {
        CharacterEvents.CharacterHurt += CharacterHurt;
        CharacterEvents.CharacterHeal += CharacterHealed;

        // new, for weapon display
        WeaponPickup.OnDisplayDroppedWeapon += DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay += StopDisplayingDroppedWeapon;
        WeaponPickup.OnDisplaySwapCharacterPrompt += DisplaySwapCharacterPrompt;

        DialogueLauncher.OnDisplayDialogue += InitiateCutScene;
        BossDialogueLauncher.OnDisplayDialogue += InitiateCutScene;
        EnemyDialogueLauncher.OnDisplayDialogue += InitiateCutScene;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterHurt -= CharacterHurt;
        CharacterEvents.CharacterHeal -= CharacterHealed;

        // new, for weapon display
        WeaponPickup.OnDisplayDroppedWeapon -= DisplayDroppedWeapon;
        WeaponPickup.OnRemoveDisplay -= StopDisplayingDroppedWeapon;
        WeaponPickup.OnDisplaySwapCharacterPrompt -= DisplaySwapCharacterPrompt;

        DialogueLauncher.OnDisplayDialogue -= InitiateCutScene;
        BossDialogueLauncher.OnDisplayDialogue -= InitiateCutScene;
        EnemyDialogueLauncher.OnDisplayDialogue -= InitiateCutScene;
    }

    public void InitiateCutScene(List<Dialogue> dialogues)
    {
        DialogueUI.SetActive(true);
        IsCutSceneOn = true;
        currDialogues = dialogues;
        //Time.timeScale = 0;
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        playerDialogueIcon.SetActive(false);
        otherDialogueIcon.SetActive(false);
        if (currDialogueIndex == currDialogues.Count - 1)
        {
            EndCutScene();
            return;
        } 
        Dialogue curr = currDialogues[++currDialogueIndex];
        DialogueText.text = curr.dialogue;
        if (curr.character == "player")
        {
            playerDialogueIcon.GetComponent<Image>().sprite = currentCharacterIcon.sprite;
            playerDialogueIcon.SetActive(true);
        } else {
            otherDialogueIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>(curr.character);
            otherDialogueIcon.SetActive(true);
        }
    }

    public void EndCutScene()
    {
        DialogueUI.SetActive(false);
        IsCutSceneOn = false;
        currDialogueIndex = -1;
        //Time.timeScale = 1;
    }

    public void DisplayPrimaryCharacterIcon(string charName, bool isActive)
    {
        string resource = charName == "dead" ? charName : charName + isActive;
        if (isActive)
        {
            currentCharacterIcon = primaryCharacterIcon;
        }
        primaryCharacterIcon.sprite = Resources.Load<Sprite>(resource);
    }

    public void DisplaySecondaryCharacterIcon(string charName, bool isActive)
    {
        string resource = charName == "dead" ? charName : charName + isActive;
        if (isActive)
        {
            currentCharacterIcon = secondaryCharacterIcon;
        }
        secondaryCharacterIcon.sprite = Resources.Load<Sprite>(resource);
    }

    public void DisplaySwapCharacterPrompt()
    {
        SwapCharacterPrompt.SetActive(true);
    }

    public void StopDisplayingDroppedWeapon()
    {
        SwapCharacterPrompt.SetActive(false);
        SwapInterface.SetActive(false);
    }

    public void DisplayDroppedWeapon(Weapon weapon)
    {
        SwapCharacterPrompt.SetActive(false);
        SwapInterface.SetActive(true);
        newWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }
    public void DisplayCurrentWeapon(Weapon weapon)
    {
        Debug.LogWarning("received weapon");
        currentWeaponIcon.sprite = Resources.Load<Sprite>(weapon.SpritePath);
    }

    public void CharacterHurt(GameObject character, int damageReceived)
    {
        // set the position of the DamageText to be where
        // the character was hit
        Vector3 _spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text _tmpText = Instantiate(DamageTextPrefab
                                , _spawnPosition
                                , Quaternion.identity
                                , HealthDamageTextManager.transform)
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

        TMP_Text _tmpText = Instantiate(HealthTextPrefab
                                , _spawnPosition
                                , Quaternion.identity
                                , HealthDamageTextManager.transform)
                                .GetComponent<TMP_Text>();

        _tmpText.text = healthRestored.ToString();
    }
}
