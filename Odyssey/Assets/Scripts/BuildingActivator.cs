using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingActivator : MonoBehaviour
{
    public GameObject BuildingPanel;
    public bool IsNearBuilding = false;
    public bool IsBuildingPanelOpen = false;

    private void Awake()
    {
        BuildingPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearBuilding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearBuilding = false;
    }

    public void OnOpenBuildingPanel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!IsNearBuilding) return;
            BuildingPanel.SetActive(!IsBuildingPanelOpen);
            IsBuildingPanelOpen = !IsBuildingPanelOpen;
        }
    }
}
