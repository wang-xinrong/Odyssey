using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsDisplay : MonoBehaviour
{
    public TMP_Text HPValue;
    public TMP_Text SPRegenValue;
    public TMP_Text SpecialAttackDamageValue;
    public TMP_Text MovementSpeedValue;
    public TMP_Text CurrentLevelText;
    public TMP_Text UpgradeCostText;
    public enum Character { MonkeyKing, Pigsy };
    public StatsManager.MKLevel MKLevel;
    public StatsManager.ZBJLevel ZBJLevel;
    public Character CardCharacter;

    private void Start()
    {
        RefreshPage();

    }

    public void RefreshPage()
    {
        MKLevel = StatsManager.Instance.CurrentMKLevel;
        ZBJLevel = StatsManager.Instance.CurrentZBJLevel;

        if (CardCharacter == Character.MonkeyKing)
        {
            HPValue.text = StatsManager.Instance
                .MKHPAndSA[MKLevel][0].ToString();

            SpecialAttackDamageValue.text = StatsManager
                .Instance.MKHPAndSA[MKLevel][1].ToString();

            SPRegenValue.text = StatsManager.Instance
                .MKSPRegenAndMovementSpeed[MKLevel][0]
                .ToString() + "/s";

            MovementSpeedValue.text = StatsManager
                .Instance.MKSPRegenAndMovementSpeed[MKLevel][1].ToString();

            if (MKLevel == StatsManager.MKLevel.Four)
            {
                CurrentLevelText.text = "Max";

                UpgradeCostText.text = "";

                return;
            }


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

            SPRegenValue.text = StatsManager
                .Instance.ZBJSPRegenAndMovementSpeed[ZBJLevel][0]
                .ToString() + "/s";

            MovementSpeedValue.text = StatsManager
                .Instance.ZBJSPRegenAndMovementSpeed[ZBJLevel][1].ToString();

            if (ZBJLevel == StatsManager.ZBJLevel.Four)
            {
                CurrentLevelText.text = "Max";

                UpgradeCostText.text = "";

                return;
            }

            CurrentLevelText.text = ZBJLevel.ToString();

            UpgradeCostText.text = StatsManager.Instance
                .ZBJUpgradeCost[ZBJLevel].ToString();
        }
    }

}
