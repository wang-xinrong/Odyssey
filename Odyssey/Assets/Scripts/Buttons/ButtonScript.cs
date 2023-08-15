using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private ButtonsManager buttonManager;
    private bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        buttonManager = GetComponentInParent<ButtonsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deselect()
    {
        isSelected = false;
    }

    public void Select()
    {
        if (!buttonManager.anyButtonPressed)
        {
            isSelected = true;
            buttonManager.anyButtonPressed = true;
        }

        if (buttonManager.anyButtonPressed && isSelected) return;

        if (buttonManager.anyButtonPressed && !isSelected)
        {
            buttonManager.DeselectAllButton();
            isSelected = true;
            buttonManager.anyButtonPressed = true;
        }
    }

    public void SetDifficultyEasy()
    {
        StatsManager.Instance.SetDifficulty(StatsManager.Difficulty.Easy);
    }

    public void SetDifficultyNormal()
    {
        StatsManager.Instance.SetDifficulty(StatsManager.Difficulty.Normal);
    }

    public void SetDifficultyHard()
    {
        StatsManager.Instance.SetDifficulty(StatsManager.Difficulty.Hard);
    }

    public void SetDifficultyExtreme()
    {
        StatsManager.Instance.SetDifficulty(StatsManager.Difficulty.Extreme);
    }
}
