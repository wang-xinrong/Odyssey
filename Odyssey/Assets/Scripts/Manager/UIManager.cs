using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject DamageText;
    public GameObject HealthText;

    private void OnEnable()
    {
        CharacterEvents.CharacterHurt += CharacterHurt;
        CharacterEvents.CharacterHeal += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.CharacterHurt -= CharacterHurt;
        CharacterEvents.CharacterHeal -= CharacterHealed;
    }



    public void CharacterHurt(GameObject character, int damageReceived)
    {
        // set the position of the DamageText to be where
        // the character was hit
        Vector3 _enablePosition = Camera.main.WorldToScreenPoint(character.transform.position);

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
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // set the position of the HealthText to be where
        // the character was hit
        Vector3 _enablePosition = Camera.main.WorldToScreenPoint(character.transform.position);
        // object pooling would most likely be needed here,
        // since there can be multiple damage source the
        // player is receiving at a time.
        DamageText.transform.position = _enablePosition;
        DamageText.GetComponent<TMP_Text>().text = healthRestored.ToString();
        HealthText.SetActive(true);
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
