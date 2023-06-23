using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterIcon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        MainPlayerController script = player.GetComponent<MainPlayerController>();
        if (!script)
        {
            return;
        }
        script.OnDisplayCurrentCharacter.AddListener(DisplayCharacterIcon);
    }

    void DisplayCharacterIcon(string charName)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>(charName);
    }
}
