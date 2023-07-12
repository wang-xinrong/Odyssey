using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private CharacterStatsDisplay display;
    private StatsManager.MKLevel nextMKLevel;
    private StatsManager.ZBJLevel nextZBJLevel;

    private void Awake()
    {
        display = GetComponentInParent<CharacterStatsDisplay>();
    }

    public void Upgrade()
    {
        if (display.CardCharacter ==
            CharacterStatsDisplay.Character.MonkeyKing)
        {
            if (StatsManager.Instance.CurrentMKLevel ==
                StatsManager.MKLevel.Four) return;

            nextMKLevel = GetNextMKLevel();

            //not enough money
            if (InventoryManager.Instance.Money <
                StatsManager.Instance.MKUpgradeCost[
                    StatsManager.Instance.CurrentMKLevel]) return;

            InventoryManager.Instance.Money -= StatsManager
                .Instance.MKUpgradeCost[StatsManager.Instance
                .CurrentMKLevel];

            StatsManager.Instance.CurrentMKLevel = nextMKLevel;
        }

        if (display.CardCharacter ==
            CharacterStatsDisplay.Character.Pigsy)
        {
            if (StatsManager.Instance.CurrentZBJLevel ==
                StatsManager.ZBJLevel.Four) return;

            nextZBJLevel = GetNextZBJLevel();

            //not enough money
            if (InventoryManager.Instance.Money <
                StatsManager.Instance.ZBJUpgradeCost[
                    StatsManager.Instance.CurrentZBJLevel]) return;

            InventoryManager.Instance.Money -= StatsManager
                .Instance.ZBJUpgradeCost[StatsManager.Instance
                .CurrentZBJLevel];

            StatsManager.Instance.CurrentZBJLevel = nextZBJLevel;
        }

        display.RefreshPage();
    }

    private StatsManager.MKLevel GetNextMKLevel()
    {
        switch (StatsManager.Instance.CurrentMKLevel)
        {
            case StatsManager.MKLevel.One:
                return StatsManager.MKLevel.Two;

            case StatsManager.MKLevel.Two:
                return StatsManager.MKLevel.Three;

            case StatsManager.MKLevel.Three:
                return StatsManager.MKLevel.Four;

            default:
                return StatsManager.MKLevel.Four;
        }
    }

    private StatsManager.ZBJLevel GetNextZBJLevel()
    {
        switch (StatsManager.Instance.CurrentZBJLevel)
        {
            case StatsManager.ZBJLevel.One:
                return StatsManager.ZBJLevel.Two;

            case StatsManager.ZBJLevel.Two:
                return StatsManager.ZBJLevel.Three;

            case StatsManager.ZBJLevel.Three:
                return StatsManager.ZBJLevel.Four;

            default:
                return StatsManager.ZBJLevel.Four;
        }
    }
}
