using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Inventory Inventory;
    public PauseMenu PauseMenu;
    public UIManager CutScene;
    public BuildingActivator Shop;
    public BuildingActivator Castle;
    public BuildingActivator Statue;
    public BuildingActivator DirectionBoard;

    public bool IsGamePaused = false;

    // Update is called once per frame
    void Update()
    {
        IsGamePaused = Inventory.IsInventoryOpen
            || PauseMenu.isPaused
            || CutScene.IsCutSceneOn;

        if (Shop && Castle && Statue)
        {
            IsGamePaused = IsGamePaused || Shop.IsBuildingPanelOpen
            || Castle.IsBuildingPanelOpen
            || Statue.IsBuildingPanelOpen
            || DirectionBoard.IsBuildingPanelOpen;
        }
        Time.timeScale = IsGamePaused ? 0 : 1;
    }
}
