using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButtonScript : MonoBehaviour
{
    public StartMenu startMenu;
    public string SceneToLoad;
    private ButtonsManager buttonsManager;

    // Start is called before the first frame update
    void Start()
    {
        buttonsManager = GetComponentInParent<ButtonsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!buttonsManager.anyButtonPressed) return;

        startMenu.LoadScene(SceneToLoad);
    }
}
