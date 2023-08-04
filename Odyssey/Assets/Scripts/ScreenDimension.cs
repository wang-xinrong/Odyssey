using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDimension : MonoBehaviour
{
    public enum OS { Windows, MacOS }
    public OS CurrOS;
    private static OS currOS;

    // new stats panel arrow navigation
    private static int statsPanelArrowWindowsOffset = 1900;
    private static int statsPanelArrowMacOSOffset = 755;

    // bottom text feedback offset
    private static float bottomTextFeedbackWindowsOffset = -270;
    private static float bottomTextFeedbackMacOSOffSet = -300;


    private void Awake()
    {
        currOS = CurrOS;
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
}
