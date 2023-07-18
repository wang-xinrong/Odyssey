using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CharacterStatsDisplay : MonoBehaviour
{
    // Current Level
    public TMP_Text HPValue;
    public TMP_Text SPRegenValue;
    public TMP_Text SpecialAttackDamageValue;
    public TMP_Text MovementSpeedValue;
    public TMP_Text CurrentLevelText;
    public TMP_Text UpgradeCostText;

    // Next Level
    public TMP_Text NextLevelHPValue;
    public TMP_Text NextLevelSPRegenValue;
    public TMP_Text NextLevelSpecialAttackDamageValue;
    public TMP_Text NextLevelMovementSpeedValue;

    public enum Character { MonkeyKing, Pigsy };
    public StatsManager.MKLevel MKLevel;
    public StatsManager.ZBJLevel ZBJLevel;
    public StatsManager.MKLevel NextMKLevel;
    public StatsManager.ZBJLevel NextZBJLevel;
    public Character CardCharacter;

    private void Start()
    {
        RefreshPage();

    }

    public void RefreshPage()
    {
        MKLevel = StatsManager.Instance.CurrentMKLevel;
        ZBJLevel = StatsManager.Instance.CurrentZBJLevel;
        NextMKLevel = StatsManager.Instance.NextMKLevel();
        NextZBJLevel = StatsManager.Instance.NextZBJLevel();

        if (CardCharacter == Character.MonkeyKing)
        {
            HPValue.text = StatsManager.Instance
                .MKHPAndSA[MKLevel][0].ToString();

            SpecialAttackDamageValue.text = StatsManager
                .Instance.MKHPAndSA[MKLevel][1].ToString();

            SPRegenValue.text = String.Format("{0:0.00}", (1 / StatsManager.Instance
                .MKSPRegenAndMovementSpeed[MKLevel][0])) + "/s";

            MovementSpeedValue.text = StatsManager
                .Instance.MKSPRegenAndMovementSpeed[MKLevel][1].ToString();

            if (MKLevel == StatsManager.MKLevel.Four)
            {
                CurrentLevelText.text = "Max";

                UpgradeCostText.text = "";

                NextLevelHPValue.text = "";
                NextLevelSPRegenValue.text = "";
                NextLevelSpecialAttackDamageValue.text = "";
                NextLevelMovementSpeedValue.text = "";

                return;
            }

            NextLevelHPValue.text = StatsManager.Instance
                .MKHPAndSA[NextMKLevel][0].ToString();

            NextLevelSpecialAttackDamageValue.text = StatsManager
                .Instance.MKHPAndSA[NextMKLevel][1].ToString();

            NextLevelSPRegenValue.text = String.Format("{0:0.00}", (1 / StatsManager.Instance
                .MKSPRegenAndMovementSpeed[NextMKLevel][0])) + "/s";

            NextLevelMovementSpeedValue.text = StatsManager
                .Instance.MKSPRegenAndMovementSpeed[NextMKLevel][1].ToString();


            CurrentLevelText.text = MKLevel.ToString();

            UpgradeCostText.text = StatsManager.Instance
                .MKUpgradeCost[MKLevel].ToString();
        }

        if (CardCharacter == Character.Pigsy)
        {
            HPValue.text = StatsManager.Instance
                .ZBJHPAndSA[ZBJLevel][0].ToString();

            SpecialAttackDamageValue.text = StatsManager
                .Instance.ZBJHPAndSA[ZBJLevel][1].ToString();

            SPRegenValue.text = String.Format("{0:0.00}", (1 / StatsManager
                .Instance.ZBJSPRegenAndMovementSpeed[ZBJLevel][0]))+ "/s";

            MovementSpeedValue.text = StatsManager
                .Instance.ZBJSPRegenAndMovementSpeed[ZBJLevel][1].ToString();

            if (ZBJLevel == StatsManager.ZBJLevel.Four)
            {
                CurrentLevelText.text = "Max";

                UpgradeCostText.text = "";

                NextLevelHPValue.text = "";
                NextLevelSPRegenValue.text = "";
                NextLevelSpecialAttackDamageValue.text = "";
                NextLevelMovementSpeedValue.text = "";

                return;
            }

            NextLevelHPValue.text = StatsManager.Instance
                .ZBJHPAndSA[NextZBJLevel][0].ToString();

            NextLevelSpecialAttackDamageValue.text = StatsManager
                .Instance.ZBJHPAndSA[NextZBJLevel][1].ToString();

            NextLevelSPRegenValue.text = String.Format("{0:0.00}", (1 / StatsManager.Instance
                .ZBJSPRegenAndMovementSpeed[NextZBJLevel][0])) + "/s";

            NextLevelMovementSpeedValue.text = StatsManager
                .Instance.ZBJSPRegenAndMovementSpeed[NextZBJLevel][1].ToString();

            CurrentLevelText.text = ZBJLevel.ToString();

            UpgradeCostText.text = StatsManager.Instance
                .ZBJUpgradeCost[ZBJLevel].ToString();
        }
    }

}
