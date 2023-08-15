using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindsPageActivation : MonoBehaviour
{
    public GameObject KeybindsPage;

    public void ActivateKeybindsPage()
    {
        KeybindsPage.SetActive(true);
    }

    public void DeactivateKeybindsPage()
    {
        KeybindsPage.SetActive(false);
    }

    private void OnEnable()
    {
        DeactivateKeybindsPage();
    }
}
