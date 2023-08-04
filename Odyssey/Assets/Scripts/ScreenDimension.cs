using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDimension : MonoBehaviour
{
    public enum OS { Windows, MacOS }
    //public OS CurrOS;
    private static OS currOS = OS.Windows;

    // new stats panel arrow navigation
    private static int statsPanelArrowWindowsOffset = 1900;
    private static int statsPanelArrowMacOSOffset = 755;

    // bottom text feedback offset
    private static float bottomTextFeedbackWindowsOffset = -270;
    private static float bottomTextFeedbackMacOSOffSet = -300;

    // Window Dimension Scale Controls
    public GameObject[] UIComponents;

    private void Awake()
    {
        //currOS = CurrOS;
    }

    private void Start()
    {
        Vector3 scale = GetScale();

        foreach (GameObject go in UIComponents)
        {
            go.transform.localScale = scale;
        }
    }

    public static float GetStatsPanelArrowOffset()
    {
        return currOS == OS.MacOS ? statsPanelArrowMacOSOffset
            : statsPanelArrowWindowsOffset;
    }

    public static float GetBottomTextFeedbackOffset()
    {
        return currOS == OS.MacOS ? bottomTextFeedbackMacOSOffSet
            : bottomTextFeedbackWindowsOffset;
    }

    private static Vector3 GetScale()
    {
        return currOS == OS.MacOS ? Vector3.one : new Vector3(0.9f, 0.9f, 1);
    }
}
