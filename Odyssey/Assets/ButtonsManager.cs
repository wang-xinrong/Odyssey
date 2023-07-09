using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public bool anyButtonPressed = false;
    public ButtonScript[] Buttons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeselectAllButton()
    {
        foreach (ButtonScript b in Buttons)
        {
            b.Deselect();
        }
        anyButtonPressed = false;
    }

    private void OnDisable()
    {
        DeselectAllButton();
    }
}
